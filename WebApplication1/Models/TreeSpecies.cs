namespace WebApplication1.Models;

using System.ComponentModel.DataAnnotations;

public class TreeSpecies
{
    [Key]
    public int SpeciesId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string LatinName { get; set; }
    
    [Required]
    public int GrowthTimeInYears { get; set; }
    
    public virtual ICollection<SeedlingBatch> SeedlingBatches { get; set; } = new List<SeedlingBatch>();
}