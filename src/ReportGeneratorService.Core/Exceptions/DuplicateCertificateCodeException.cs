namespace ReportGeneratorService.Core.Exceptions;

public class DuplicateCertificateCodeException : DomainException
{
    public string Code { get; }

    public DuplicateCertificateCodeException(string code)
        : base($"Certificate code '{code}' already exists.")
    {
        Code = code;
    }
}