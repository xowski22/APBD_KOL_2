using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    
    public DbSet<Nursery> Nurseries { get; set; }
    public DbSet<TreeSpecies> TreeSpecies { get; set; }
    public DbSet<Employee> Employees { get; set; }
    public DbSet<SeedlingBatch> SeedlingBatches { get; set; }
    public DbSet<Responsible> Responsibles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Responsible>()
            .HasKey(r => new { r.BatchId, r.EmployeeId });
        
        modelBuilder.Entity<SeedlingBatch>()
            .HasOne(sb => sb.Nursery)
            .WithMany(n => n.SeedlingBatches)
            .HasForeignKey(sb => sb.NurseryId);
        
        modelBuilder.Entity<SeedlingBatch>()
            .HasOne(sb => sb.TreeSpecies)
            .WithMany(ts => ts.SeedlingBatches)
            .HasForeignKey(sb => sb.SpeciesId);
        
        modelBuilder.Entity<Responsible>()
            .HasOne(r => r.SeedlingBatch)
            .WithMany(sb => sb.ResponsibleEmployees)
            .HasForeignKey(r => r.BatchId);
        
        modelBuilder.Entity<Responsible>()
            .HasOne(r => r.Employee)
            .WithMany(e => e.Responsibilities)
            .HasForeignKey(r => r.EmployeeId);
        
        modelBuilder.Entity<Nursery>().HasData(
            new Nursery { NurseryId = 1, Name = "Green Forest Nursery", EstablishedDate = new DateTime(2005, 5, 10) },
            new Nursery { NurseryId = 2, Name = "Pine Valley Nursery", EstablishedDate = new DateTime(2010, 8, 15) }
        );
        
        modelBuilder.Entity<TreeSpecies>().HasData(
            new TreeSpecies { SpeciesId = 1, LatinName = "Quercus robur", GrowthTimeInYears = 5 },
            new TreeSpecies { SpeciesId = 2, LatinName = "Pinus sylvestris", GrowthTimeInYears = 3 },
            new TreeSpecies { SpeciesId = 3, LatinName = "Fagus sylvatica", GrowthTimeInYears = 4 }
        );

        modelBuilder.Entity<Employee>().HasData(
            new Employee { EmployeeId = 1, FirstName = "Anna", LastName = "Kowalska", HireDate = new DateTime(2020, 3, 1) },
            new Employee { EmployeeId = 2, FirstName = "Jan", LastName = "Nowak", HireDate = new DateTime(2019, 6, 15) },
            new Employee { EmployeeId = 3, FirstName = "Maria", LastName = "Wiśniewska", HireDate = new DateTime(2021, 1, 10) }
        );
        
        modelBuilder.Entity<SeedlingBatch>().HasData(
            new SeedlingBatch 
            { 
                BatchId = 1, 
                NurseryId = 1, 
                SpeciesId = 1, 
                Quantity = 500, 
                SownDate = new DateTime(2024, 3, 15), 
                ReadyDate = new DateTime(2029, 3, 15) 
            },
            new SeedlingBatch 
            { 
                BatchId = 2, 
                NurseryId = 1, 
                SpeciesId = 2, 
                Quantity = 300, 
                SownDate = new DateTime(2024, 4, 1), 
                ReadyDate = null
            }
        );
        
        modelBuilder.Entity<Responsible>().HasData(
            new Responsible { BatchId = 1, EmployeeId = 1, Role = "Supervisor" },
            new Responsible { BatchId = 1, EmployeeId = 2, Role = "Planter" },
            new Responsible { BatchId = 2, EmployeeId = 3, Role = "Supervisor" }
        );
        
        
    }
}