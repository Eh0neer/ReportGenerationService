namespace ReportGeneratorService.Core.Interfaces;

public interface IAuditable
{
    DateTime CreatedAt { get; }
    DateTime? UpdatedAt { get; }
    
    void SetCreatedAt(DateTime createdAt);
    void SetUpdatedAt(DateTime? updatedAt);
}

public interface ISoftDeletable
{
    bool IsDeleted { get; }
    void MarkAsDeleted();
}