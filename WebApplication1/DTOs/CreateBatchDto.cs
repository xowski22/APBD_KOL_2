namespace WebApplication1.DTOs;

using System.ComponentModel.DataAnnotations;

public class CreateBatchDto
{
    [Required]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be greater than 0")]
    public int Quantity { get; set; }
    
    [Required]
    public string Species { get; set; }
    
    [Required]
    public string Nursery { get; set; }
    
    [Required]
    public List<ResponsibleCreateDto> Responsible { get; set; } = new List<ResponsibleCreateDto>();
}

public class ResponsibleCreateDto
{
    [Required]
    public int EmployeeId { get; set; }
    
    [Required]
    public string Role { get; set; }
}