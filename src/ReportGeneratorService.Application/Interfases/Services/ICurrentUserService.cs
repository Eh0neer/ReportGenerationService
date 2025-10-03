namespace ReportGeneratorService.Application.Interfases.Services;

public interface ICurrentUserService
{
    string? UserId { get; }
    string? Email { get; }
    bool IsAuthenticated { get; }
    IEnumerable<string> Roles { get; }
    
    bool IsInRole(string role);
    bool HasPermission(string permission);
}