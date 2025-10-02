namespace ReportGeneratorService.Application.UseCases.Auth.Login;

public record LoginResponse
{
    public string Token { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public DateTime ExpiresAt { get; init; }
}