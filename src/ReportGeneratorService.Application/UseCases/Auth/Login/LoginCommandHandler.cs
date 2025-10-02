using MediatR;
using Microsoft.Extensions.Logging;
using ReportGeneratorService.Application.DTOs.Responses;
using ReportGeneratorService.Core.Interfaces.Repositories;
using ReportGeneratorService.Core.Interfaces.Services;

namespace ReportGeneratorService.Application.UseCases.Auth.Login;

public class LoginCommandHandler : IRequestHandler<LoginCommand, Result<LoginResponse>>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IMfaService _mfaService;
    private readonly ITokenService _tokenService;
    private readonly ILogger<LoginCommandHandler> _logger;

    public LoginCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IMfaService mfaService,
        ITokenService tokenService,
        ILogger<LoginCommandHandler> logger)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _mfaService = mfaService;
        _tokenService = tokenService;
        _logger = logger;
    }

    public async Task<Result<LoginResponse>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        try
        {
            _logger.LogInformation("Login attempt for email: {email}", request.Email);
            if (string.IsNullOrWhiteSpace(request.Email) || string.IsNullOrWhiteSpace(request.Password))
            {
                return Result<LoginResponse>.Failure("Email and password are required");
            }

            var user = await _userRepository.GetByEmailAsync(request.Email, cancellationToken);

            //Check user
            if (user == null)
            {
                _logger.LogWarning("Login failure: User not found for email {Email}", request.Email);

                return Result<LoginResponse>.Failure("Invalid credentials");
            }

            //Check password
            if (!user.VerifyPassword(request.Password, _passwordHasher))
            {
                _logger.LogWarning("Login failed: Invalid password for email {Email}", request.Email);

                return Result<LoginResponse>.Failure("Invalid credentials");
            }

            //Check mfacode
            if (!user.VerifyMfaCode(request.MfaCode, _mfaService))
            {
                _logger.LogWarning("Login failed: Invalid MFA code for email {Email}", request.Email);

                return Result<LoginResponse>.Failure("Invalid MFA code");
            }

            // token generate
            var token = _tokenService.GenerateToken(user.Id.ToString());
            var expireAt = DateTime.UtcNow.AddHours(1); // how log token is valid

            _logger.LogInformation("Login successful for email {Email}", request.Email);

            return Result<LoginResponse>.Success(new LoginResponse
            {
                Token = token,
                Email = user.Email,
                ExpiresAt = expireAt
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error during login for email: {Email}", request.Email);
            return Result<LoginResponse>.Failure("An error occurred during login");
        }
    }
}