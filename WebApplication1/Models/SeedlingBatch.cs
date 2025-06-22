using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace WebApplication1.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class SeedlingBatch
{
    [Key]
    public int BatchId { get; set; }
    
    [ForeignKey("Nursery")]
    public int NurseryId { get; set; }
    
    [ForeignKey("TreeSpecies")]
    public int SpeciesId { get; set; }
    
    [Required]
    public int Quantity { get; set; }
    
    [Required]
    public DateTime SownDate { get; set; }
    
    public DateTime? ReadyDate { get; set; }
    
    public virtual Nursery Nursery { get; set; }
    public virtual TreeSpecies TreeSpecies { get; set; }
    
    public virtual ICollection<Responsible> ResponsibleEmployees { get; set; } = new List<Responsible>();
}
