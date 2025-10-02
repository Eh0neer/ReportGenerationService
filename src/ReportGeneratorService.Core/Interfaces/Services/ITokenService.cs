namespace ReportGeneratorService.Core.Interfaces.Services;

public interface ITokenService
{
    string GenerateToken(string userId);
    bool ValidateToken(string token);
    string GetUserIdFromToken(string token);
}