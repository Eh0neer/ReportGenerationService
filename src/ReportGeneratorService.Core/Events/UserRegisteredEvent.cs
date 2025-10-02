using ReportGeneratorService.Core.Entities;

namespace ReportGeneratorService.Core.Events;

public class UserRegisteredEvent : IDomainEvent
{
    public User User { get; }
    public DateTime OccurredOn { get; }

    public UserRegisteredEvent(User user)
    {
        User = user;
        OccurredOn = DateTime.Now;
    }
}