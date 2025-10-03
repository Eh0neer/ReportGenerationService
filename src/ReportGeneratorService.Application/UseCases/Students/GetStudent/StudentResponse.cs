using ReportGeneratorService.Core.Enums;

namespace ReportGeneratorService.Application.UseCases.Students.GetStudent;

public record StudentResponse
{
    public string StudentId { get; init; } = string.Empty;
    public string LastName { get; init; } = string.Empty;
    public string FirstName { get; init; } = string.Empty;
    public string? SecondName { get; init; }
    public string FullName { get; init; } = string.Empty;
    public DateOnly BirthDate { get; init; }
    public int Age { get; init; }
    public int Course { get; init; }
    public string GroupNumber { get; init; } = string.Empty;
    public string Faculty { get; init; } = string.Empty;
    public string Profile { get; init; } = string.Empty;
    public string Direction { get; init; } = string.Empty;
    public EducationForm EduForm { get; init; }
    public EducationLevel EduLevel { get; init; }
    public StudentStatus Status { get; init; }
    public bool CanGenerateCertificate { get; init; }
    public int ContingentId { get; init; }
}