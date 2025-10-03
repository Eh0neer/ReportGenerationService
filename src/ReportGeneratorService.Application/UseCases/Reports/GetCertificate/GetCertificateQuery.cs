using MediatR;
using ReportGeneratorService.Application.DTOs.Responses;

namespace ReportGeneratorService.Application.UseCases.Reports.GetCertificate;

public record GetCertificateQuery : IRequest<Result<CertificateDetailsResponse>>
{
    public string Code { get; init; } = string.Empty;
}