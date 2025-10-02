namespace ReportGeneratorService.Core.Events;

public interface IDomainEvent
{
    DateTime OccurredOn { get; }
}