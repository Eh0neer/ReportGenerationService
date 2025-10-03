using ReportGeneratorService.Core.Enums;

namespace ReportGeneratorService.Application.UseCases.Reports.GetCertificate;

public record CertificateDetailsResponse
{
    public string Code { get; init; } = string.Empty;
    public CertificateType Type { get; init; }
    public DateTime GeneratedAt { get; init; }
    public string StudentName { get; init; } = string.Empty;
    public string StudentGroup {get; init; } = string.Empty;
    public string Faculty { get; init; } = string.Empty;
    public bool IsExpired {get; init; }
}