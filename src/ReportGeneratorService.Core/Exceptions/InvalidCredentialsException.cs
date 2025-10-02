namespace ReportGeneratorService.Core.Exceptions;

public class InvalidCredentialsException : DomainException
{
    public string Email { get; }

    public InvalidCredentialsException(string email)
        : base($"Invalid credentials for user '{email}'.")
    {
        Email = email;
    }
}