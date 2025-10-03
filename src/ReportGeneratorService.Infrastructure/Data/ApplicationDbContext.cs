using Microsoft.EntityFrameworkCore;
using ReportGeneratorService.Core.Entities;
using ReportGeneratorService.Core.ValueObjects;
using System.Reflection;
using ReportGeneratorService.Core.Interfaces;

namespace ReportGeneratorService.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
    
    //DbSets
    
    public DbSet<User> Users => Set<User>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Certificate> Certificates => Set<Certificate>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        // global filters soft delete
        modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
        modelBuilder.Entity<Student>().HasQueryFilter(s => !s.IsDeleted);
        modelBuilder.Entity<Certificate>().HasQueryFilter(c => !c.IsDeleted);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is IAuditable auditable)
            {
                if (entry.State == EntityState.Added)
                {
                    auditable.SetCreatedAt(DateTime.UtcNow);
                }
                auditable.SetUpdatedAt(DateTime.UtcNow);
            }

            if (entry.Entity is BaseEntity baseEntity)
            {
                if (entry.State == EntityState.Added)
                {
                    // Уже установлено через IAuditable
                }
                baseEntity.UpdateTimestamps();
            }
        }

        return await base.SaveChangesAsync(cancellationToken);
    }

    public override int SaveChanges()
    {
        var entries = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

        foreach (var entry in entries)
        {
            if (entry.Entity is IAuditable auditable)
            {
                if (entry.State == EntityState.Added)
                {
                    auditable.SetCreatedAt(DateTime.UtcNow);
                }
                auditable.SetUpdatedAt(DateTime.UtcNow);
            }

            if (entry.Entity is BaseEntity baseEntity)
            {
                baseEntity.UpdateTimestamps();
            }
        }

        return base.SaveChanges();
    }
}