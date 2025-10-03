using MediatR;
using ReportGeneratorService.Application.DTOs.Responses;
using ReportGeneratorService.Core.Enums;

namespace ReportGeneratorService.Application.UseCases.Reports.GenerateReport;

public class GenerateReportCommand : IRequest<Result<ReportResponse>>
{
    public int ContingentId { get; init; }
    public string Role { get; init; } = string.Empty;
    public CertificateType CertificateType { get; init; }
    public string Format { get; init; } = "pdf";
}