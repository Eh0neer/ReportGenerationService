namespace ReportGeneratorService.Core.Interfaces.Services;

public interface ICertificateCodeGenerator
{
    Task<string> GenerateUniqueCodeAsync();
    string GenerateRandomCode();
}