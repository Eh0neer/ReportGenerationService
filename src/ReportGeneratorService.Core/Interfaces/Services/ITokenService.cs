namespace ReportGeneratorService.Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken();
    bool ValidateToken(string token);
    string GetUserIdFromToken(string token);
}