using MediatR;
using Microsoft.Extensions.Logging;
using ReportGeneratorService.Application.DTOs.Responses;
using ReportGeneratorService.Core.Exceptions;
using ReportGeneratorService.Core.Interfaces.Repositories;
using ReportGeneratorService.Core.Interfaces.Services;

namespace ReportGeneratorService.Application.UseCases.Reports.GenerateReport;

public class GenerateReportCommandHandler : IRequestHandler<GenerateReportCommand, Result<ReportResponse>>
{
    private readonly IStudentRepository _studentRepository;
    private readonly IReportGenerator _reportGenerator;
    private readonly ICertificateCodeGenerator _certificateCodeGenerator;
    private readonly ICertificateRepository _certificateRepository;
    private readonly ILogger<GenerateReportCommandHandler> _logger;

    public GenerateReportCommandHandler(
        IStudentRepository studentRepository,
        IReportGenerator reportGenerator,
        ICertificateCodeGenerator certificateCodeGenerator,
        ICertificateRepository certificateRepository,
        ILogger<GenerateReportCommandHandler> logger)
    {
        _studentRepository = studentRepository;
        _reportGenerator = reportGenerator;
        _certificateCodeGenerator = certificateCodeGenerator;
        _certificateRepository = certificateRepository;
        _logger = logger;
    }

    public async Task<Result<ReportResponse>> Handle(GenerateReportCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation(
                "Generate report for contingent ID: {ContingentId}, Type: {CertificationType}, Format: {Format}",
                request.ContingentId, request.CertificateType, request.Format);

            //check student
            var student = await _studentRepository.GetByContingentIdAsync(request.ContingentId, cancellationToken);

            if (student == null)
            {
                _logger.LogWarning("Student not found for contingent ID: {ContingentId}", request.ContingentId);
                return Result<ReportResponse>.Failure("Student not found");
            }

            //check role 
            if (!HasPermission(request.Role, request.CertificateType))
            {
                _logger.LogWarning(
                    "Permission denied for role {Role} to generate certificate type {CertificationType}", request.Role,
                    request.CertificateType);
                return Result<ReportResponse>.Failure("Insufficient permissions");
            }

            // Generate unique certificate code

            var certificateCode = await _certificateCodeGenerator.GenerateUniqueCodeAsync();

            // File generation
            byte[] fileContent;
            string contentType;
            string fileName;

            if (request.Format.Equals("pdf", StringComparison.InvariantCultureIgnoreCase))
            {
                fileContent =
                    await _reportGenerator.GeneratePdfAsync(student, request.CertificateType, certificateCode);
                contentType = "application/pdf";
                fileName = $"certificate_{certificateCode}.pdf";
            }
            else if (request.Format.Equals("docx", StringComparison.OrdinalIgnoreCase))
            {
                var pdfByte =
                    await _reportGenerator.GeneratePdfAsync(student, request.CertificateType, certificateCode);
                fileContent = await _reportGenerator.GenerateDocxAsync(pdfByte);
                contentType = "application/vnd.openxmlformats-officedocument.wordprocessingml.document";
                fileName = $"certificate_{certificateCode}.docx";
            }
            else
            {
                return Result<ReportResponse>.Failure($"Unsupported format: {request.Format}. Use 'pdf' or 'docx'.");
            }

            // Saving information about the help
            var certificate = Core.Entities.Certificate.Create(certificateCode, request.CertificateType, student);

            await _certificateRepository.AddAsync(certificate, cancellationToken);

            _logger.LogInformation(
                "Report generated successfully for contingent ID: {ContingentId}, Code: {CertificateCode}",
                request.ContingentId, certificateCode);

            return Result<ReportResponse>.Success(new ReportResponse
            {
                Content = fileContent,
                ContentType = contentType,
                FileName = fileName,
                CetificateCode = certificateCode,
                GeneratedAt = DateTime.UtcNow
            });
        }
        catch (InvalidCertificateGenerationException ex)
        {
            _logger.LogWarning(ex, "Certificate generation validate failed for contingent ID: {ContingentId}"
                , request.ContingentId);

            return Result<ReportResponse>.Failure(ex.Message);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error generating report for contingent ID: {ContingentId}",
                request.ContingentId);
            
            return Result<ReportResponse>.Failure("An error occurred while generating the report");
        }
    }

    private static bool HasPermission(string role, Core.Enums.CertificateType certificateType)
    {
        return role.ToLower() switch
        {
            "admin" => true,
            "dean" => certificateType != Core.Enums.CertificateType.Expelled,
            "secretary" => certificateType == Core.Enums.CertificateType.Regular,
            _ => false
        };
    }
}