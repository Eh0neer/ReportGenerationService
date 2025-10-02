namespace ReportGeneratorService.Core.Exceptions;

public class InvalidCertificateGenerationException : DomainException
{
    public string StudentId { get; }
    public string Reason { get; }

    public InvalidCertificateGenerationException(string sutdentId, string reason)
        : base($"Cannot generate certificate for student '{sutdentId}': {reason}")
    {
        StudentId = sutdentId;
        Reason = reason;
    }
}