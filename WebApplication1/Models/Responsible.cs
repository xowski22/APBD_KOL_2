namespace WebApplication1.Models;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Responsible
{
    [Key]
    [Column(Order = 0)]
    [ForeignKey("SeedlingBatch")]
    public int BatchId { get; set; }
    
    [Key]
    [Column(Order = 1)]
    [ForeignKey("Employee")]
    public int EmployeeId { get; set; }
    
    [Required]
    [MaxLength(100)]
    public string Role { get; set; }
    
    public virtual SeedlingBatch SeedlingBatch { get; set; }
    public virtual Employee Employee { get; set; }
}