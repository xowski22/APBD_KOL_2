namespace WebApplication1.Models;

using System.ComponentModel.DataAnnotations;

public class Nursery
{
    [Key]
    public int NurseryId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }
    
    [Required]
    public DateTime EstablishedDate { get; set; }
    
    public virtual ICollection<SeedlingBatch> SeedlingBatches { get; set; } = new List<SeedlingBatch>();
}