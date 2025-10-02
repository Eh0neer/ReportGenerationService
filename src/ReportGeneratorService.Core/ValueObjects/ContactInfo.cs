namespace ReportGeneratorService.Core.ValueObjects;

public class ContactInfo : ValueObject
{
    public string Email { get; private set; }
    public string Phone { get; private set; }
    public ContactType Type { get; private set; }
    public bool IsPrimary { get; private set; }

    private ContactInfo() { }

    public ContactInfo(string email, string phone, ContactType type, bool isPrimary = false)
    {
        if(!IsValidEmail(email))
            throw new ArgumentException("Invalid email format", nameof(email));
        if(!IsValidPhone(phone))
            throw new ArgumentException("Invalid phone format", nameof(phone));
        
        Email = email.Trim().ToLower();
        Phone = NormalizePhone(phone);
        Type = type;
        IsPrimary = isPrimary;
        
        
    }

    private static bool IsValidEmail(string email)
    {
        try
        {
            var addr = new System.Net.Mail.MailAddress(email);
            return addr.Address == email;
        }
        catch
        {
            return false;
        }
    }

    private static bool IsValidPhone(string phone)
    {
        return !string.IsNullOrWhiteSpace(phone) && phone.Length == 11;
    }

    private static string NormalizePhone(string phone)
    {
        return System.Text.RegularExpressions.Regex.Replace(phone, @"[^\d+]", "");
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Email;
        yield return Phone;
        yield return Type;
    }
}

public enum ContactType
{
    Personal,
    Work,
    Emergency
}