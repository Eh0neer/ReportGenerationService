using ReportGeneratorService.Core.Enums;
using ReportGeneratorService.Core.ValueObjects;

namespace ReportGeneratorService.Core.Entities;

public class Student : Entity<string>
{
    public string LastName { get; private set; }
    public string FirstName { get; private set; }
    public string? SecondName { get; private set; }
    public DateOnly BirthDate { get; private set; }
    public int Course { get; private set; }
    public string GroupNumber { get; private set; }
    public string Faculty { get; private set; }
    public string Profile { get; private set; }
    public string Direction { get; private set; }
    public EducationForm EduForm { get; private set; }
    public EducationLevel EduLevel { get; private set; }
    public int StartYear { get; private set; }
    public string CreditBookNumber { get; private set; }
    public StudentStatus Status { get; private set; }
    public string OrderNumber { get; private set; }
    public DateTime OrderDate { get; private set; }
    public int ContingentId { get; private set; }
    
    //navigate for ValueObject
    private readonly List<ContactInfo> _contactInfos = new();
    public IReadOnlyCollection<ContactInfo> ContactInfos => _contactInfos.AsReadOnly();

    private readonly List<Passport> _passports = new();
    public IReadOnlyCollection<Passport> Passports => _passports.AsReadOnly();
    
    private Student() { }

    public Student(
        string studentId,
        string lastName,
        string firstName,
        string? secondName,
        DateOnly birthDate,
        int course,
        string groupNumber,
        string faculty,
        string profile,
        string direction,
        EducationForm eduForm,
        EducationLevel eduLevel,
        int startYear,
        string creditBookNumber,
        StudentStatus status,
        string orderNumber,
        DateTime orderDate,
        int contingentId)
    {
        Id = studentId ?? throw new ArgumentNullException(nameof(studentId));
        LastName = lastName ?? throw new ArgumentNullException(nameof(lastName));
        FirstName = firstName ?? throw new ArgumentNullException(nameof(firstName));
        SecondName = secondName;
        BirthDate = birthDate;
        Course = course;
        GroupNumber = groupNumber ?? throw new ArgumentNullException(nameof(groupNumber));
        Faculty = faculty ?? throw new ArgumentNullException(nameof(faculty));
        Profile = profile ?? throw new ArgumentNullException(nameof(profile));
        Direction = direction ?? throw new ArgumentNullException(nameof(direction));
        EduForm = eduForm;
        EduLevel = eduLevel;
        StartYear = startYear;
        CreditBookNumber = creditBookNumber ?? throw new ArgumentNullException(nameof(creditBookNumber));
        Status = status;
        OrderNumber = orderNumber ?? throw new ArgumentNullException(nameof(orderNumber));
        OrderDate = orderDate;
        ContingentId = contingentId;
    }

    public string GetFullName()
    {
        return $"{LastName} {FirstName} {SecondName}".Trim();
    }

    public int GetCurrentAge()
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - BirthDate.Year;
        if (BirthDate > today.AddYears(-age)) age--;
        return age;
    }

    public bool CanGenerateCertificate()
    {
        return Status != StudentStatus.Expelled &&
               Status != StudentStatus.AcademicLeave &&
               Status != StudentStatus.Graduated &&
               Status != StudentStatus.Transferred;
    }

    public void AddContactInfo(ContactInfo contactInfo)
    {
        if (contactInfo == null)
            throw new ArgumentNullException(nameof(contactInfo));
        
        _contactInfos.Add(contactInfo);
    }

    public void AddPassport(Passport passport)
    {
        if (passport == null)
            throw new ArgumentNullException(nameof(passport));
        
        _passports.Add(passport);
    }
}