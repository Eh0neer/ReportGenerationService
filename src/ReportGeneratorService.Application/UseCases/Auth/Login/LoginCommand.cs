using MediatR;
using ReportGeneratorService.Application.DTOs.Responses;

namespace ReportGeneratorService.Application.UseCases.Auth.Login;

public record LoginCommand : IRequest<Result<LoginResponse>>
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string MfaCode { get; init; } = string.Empty;
}