namespace ReportGeneratorService.Core.Exceptions;

public class StudentNotFoundException : DomainException
{
    public string StudentId { get; }

    public StudentNotFoundException(string studentId)
        : base($"Student with ID {studentId} was not found.")
    {
        StudentId = studentId;
    }

    public StudentNotFoundException(int contingentId)
        : base($"Student with ContingentID {contingentId} was not found.")
    {
        StudentId = contingentId.ToString();
    }
}