namespace WebApplication1.Controllers;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.DTOs;

[ApiController]
[Route("api/[controller]")]
public class NurseriesController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public NurseriesController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet("{id}/batches")]
    public async Task<ActionResult<NurseryBatchesDto>> GetNurseryBatches(int id)
    {
        var nursery = await _context.Nurseries
            .Include(n => n.SeedlingBatches)
            .ThenInclude(sb => sb.TreeSpecies)
            .Include(n => n.SeedlingBatches)
            .ThenInclude(sb => sb.ResponsibleEmployees)
            .ThenInclude(r => r.Employee)
            .FirstOrDefaultAsync(n => n.NurseryId == id);
        
        if (nursery == null)
            return NotFound("Nie znaleziono id");

        var result = new NurseryBatchesDto
        {
            NurseryId = nursery.NurseryId,
            Name = nursery.Name,
            EstablishedDate = nursery.EstablishedDate,
            Batches = nursery.SeedlingBatches.Select(sb => new BatchDto
            {
                BatchId = sb.BatchId,
                Quantity = sb.Quantity,
                SownDate = sb.SownDate,
                ReadyDate = sb.ReadyDate,
                Species = new SpeciesDto
                {
                    LatinName = sb.TreeSpecies.LatinName,
                    GrowthTimeInYears = sb.TreeSpecies.GrowthTimeInYears
                },
                Responsible = sb.ResponsibleEmployees.Select(r => new ResponsibleDto
                {
                    FirstName = r.Employee.FirstName,
                    LastName = r.Employee.LastName,
                    Role = r.Role
                }).ToList()
            }).ToList()
        };
        
        return Ok(result);
    }
}