namespace ReportGeneratorService.Core.ValueObjects;

public class Passport : ValueObject
{
    public string Serial { get; private set; }
    public string Number { get; private set; }
    public string IssuedBy { get; private set; }
    public DateOnly IssueDate { get; private set; }
    public PassportType Type { get; private set; }

    private Passport() { }

    public Passport(string serial, string number, string issuedBy, DateOnly issueDate, PassportType type)
    {
        if(string.IsNullOrWhiteSpace(serial))
            throw new ArgumentException("Serial cannot be empty", nameof(serial));
        
        if(string.IsNullOrWhiteSpace(number))
            throw new ArgumentException("Number cannot be empty", nameof(number));
        
        if(string.IsNullOrWhiteSpace(issuedBy))
            throw new ArgumentException("IssuedBy cannot be empty", nameof(issuedBy));
        
        Serial = serial.Trim();
        Number = number.Trim();
        IssuedBy = issuedBy.Trim();
        IssueDate = issueDate;
        Type = type;
    }

    public string GetFullNumber()
    {
        return $"{Serial} {Number}";
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Serial;
        yield return Number;
        yield return IssuedBy;
        yield return IssueDate;
        yield return Type;
    }
    
}

public enum PassportType
{
    Internal,    // Внутренний паспорт
    International // Заграничный паспорт
}