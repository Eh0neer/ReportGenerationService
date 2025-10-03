namespace ReportGeneratorService.Application.UseCases.Reports.GenerateReport;

public record ReportResponse
{
    public byte[] Content { get; init; } = Array.Empty<byte>();
    public string ContentType { get; init; } = string.Empty;
    public string FileName { get; init; } = string.Empty;
    public string CetificateCode { get; init; } = string.Empty;
    public DateTime GeneratedAt { get; init; }
}