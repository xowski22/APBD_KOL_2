namespace WebApplication1.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;
using WebApplication1.Models;

[ApiController]
[Route("api/[controller]")]
public class BatchesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public BatchesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<ActionResult> CreateBatch([FromBody] CreateBatchDto dto)
    {
        if(!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var species = await _context.TreeSpecies
            .FirstOrDefaultAsync(ts => ts.LatinName == dto.Species);
        
        if (species == null)
            return BadRequest("Species not found");
        
        var nursery = await _context.Nurseries
            .FirstOrDefaultAsync(n => n.Name == dto.Nursery);
        
        if(nursery == null)
            return BadRequest("Nursery not found");
        
        var employeeIds = dto.Responsible.Select(r => r.EmployeeId).ToList();
        
        var existingEmployees = await _context.Employees
            .Where(e => employeeIds.Contains(e.EmployeeId))
            .ToListAsync();
        
        if (existingEmployees.Count != employeeIds.Count)
        {
            var missingIds = employeeIds.Except(existingEmployees.Select(e => e.EmployeeId)).ToList();
            return BadRequest("Employees do not exist");
        }

        var batch = new SeedlingBatch
        {
            NurseryId = nursery.NurseryId,
            SpeciesId = species.SpeciesId,
            Quantity = dto.Quantity,
            SownDate = DateTime.Now,
            ReadyDate = DateTime.Now.AddYears(species.GrowthTimeInYears)
        };
        
        _context.SeedlingBatches.Add(batch);
        await _context.SaveChangesAsync();
        var responsibleList = new List<Responsible>();
        foreach (var responsibleDto in dto.Responsible)
        {
            responsibleList.Add(new Responsible
            {
                BatchId = batch.BatchId,                
                EmployeeId = responsibleDto.EmployeeId, 
                Role = responsibleDto.Role             
            });
        }
        
        _context.Responsibles.AddRange(responsibleList);
        await _context.SaveChangesAsync();
        
        return Ok(new { BatchId = batch.BatchId, Message = "Batch created successfully" });
    }
}