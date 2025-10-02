using ReportGeneratorService.Core.Entities;
using ReportGeneratorService.Core.Enums;

namespace ReportGeneratorService.Core.Interfaces.Services;

public interface IReportGenerator
{
    Task<byte[]> GeneratePdfAsync(Student student, CertificateType type, string certificateCode);
    Task<byte[]> GenerateDocxAsync(byte[] pdfBytes);
    Task<bool> ValidateTemplateExistAsync(CertificateType type);
}