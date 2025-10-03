using System.Runtime.CompilerServices;
using ReportGeneratorService.Core.Interfaces;

namespace ReportGeneratorService.Core.Entities;
public abstract class BaseEntity : IAuditable, ISoftDeletable
{
    public bool IsDeleted { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }
    protected BaseEntity()
    {
        CreatedAt = DateTime.UtcNow;
    }

    // Реализация IAuditable
    void IAuditable.SetCreatedAt(DateTime createdAt)
    {
        CreatedAt = createdAt;
    }

    void IAuditable.SetUpdatedAt(DateTime? updatedAt)
    {
        UpdatedAt = updatedAt;
    }

    // Реализация ISoftDeletable
    public virtual void MarkAsDeleted()
    {
        IsDeleted = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public virtual void UpdateTimestamps()
    {
        UpdatedAt = DateTime.UtcNow;
    }
}
