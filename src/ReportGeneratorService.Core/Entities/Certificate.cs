using ReportGeneratorService.Core.Enums;

namespace ReportGeneratorService.Core.Entities;

public class Certificate : Entity<int>
{
    public string Code { get; private set; }
    public CertificateType Type { get; private set; }
    public DateTime GeneratedAt { get; private set; }
    public string StudentId { get; private set; }
    public Student Student { get; private set; } // Navigation property

    private Certificate() { }

    private Certificate(string code, CertificateType type, string studentId)
    {
        Code = code ?? throw new ArgumentNullException(nameof(code));
        Type = type;
        GeneratedAt = DateTime.UtcNow;
        StudentId = studentId ?? throw new ArgumentNullException(nameof(studentId));
    }

    public static Certificate Create(string code, CertificateType type, Student student)
    {
        if (student == null)
            throw new ArgumentNullException(nameof(student));
            
        if (!student.CanGenerateCertificate())
            throw new InvalidOperationException($"Cannot generate certificate for student with status: {student.Status}");

        return new Certificate(code, type, student.Id);
    }

    public bool IsExpired()
    {
        // Срок действия спраки
        return GeneratedAt.AddDays(30) < DateTime.UtcNow;
    }
}