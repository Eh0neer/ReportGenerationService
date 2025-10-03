using MediatR;
using Microsoft.Extensions.Logging;
using ReportGeneratorService.Application.DTOs.Responses;
using ReportGeneratorService.Core.Interfaces.Repositories;

namespace ReportGeneratorService.Application.UseCases.Reports.GetCertificate;

public class GetCertificateQueryHandler : IRequestHandler<GetCertificateQuery, Result<CertificateDetailsResponse>>
{
    private readonly ICertificateRepository _certificateRepository;
    private readonly ILogger<GetCertificateQueryHandler> _logger;

    public GetCertificateQueryHandler(
        ICertificateRepository certificateRepository,
        ILogger<GetCertificateQueryHandler> logger)
    {
        _certificateRepository = certificateRepository;
        _logger = logger;
    }

    public async Task<Result<CertificateDetailsResponse>> Handle(GetCertificateQuery request,
        CancellationToken cancellationToken)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(request.Code))
            {
                return Result<CertificateDetailsResponse>.Failure("Certificate code is required");
            }
            
            var certificate = await _certificateRepository.GetByCodeAsync(request.Code, cancellationToken);
            if (certificate is null)
            {
                return Result<CertificateDetailsResponse>.Failure("Certificate not found");
            }

            var response = new CertificateDetailsResponse
            {
                Code = certificate.Code,
                Type = certificate.Type,
                GeneratedAt = certificate.GeneratedAt,
                StudentName = certificate.Student.GetFullName(),
                StudentGroup = certificate.Student.GroupNumber,
                Faculty = certificate.Student.Faculty,
                IsExpired = certificate.IsExpired()
            };
            
            return Result<CertificateDetailsResponse>.Success(response);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving certificate with code: {Code}", request.Code);

            return Result<CertificateDetailsResponse>.Failure("An error while retrieving certificate details");
        }
    }
}