using ReportGeneratorService.Core.Entities;

namespace ReportGeneratorService.Core.Events;

public class CertificateGeneratedEvent : IDomainEvent
{
    public Certificate Certificate { get; }
    public DateTime OccurredOn { get; }

    public CertificateGeneratedEvent(Certificate certificate)
    {
        Certificate = certificate;
        OccurredOn = DateTime.Now;
    }
}