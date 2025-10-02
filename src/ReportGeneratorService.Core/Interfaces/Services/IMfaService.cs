namespace ReportGeneratorService.Core.Interfaces.Services;

public interface IMfaService
{
    string GenerateSecret();
    bool ValidateCode(string secret, string code);
    string GeneratorQrCodeUri(string secret, string email);
}