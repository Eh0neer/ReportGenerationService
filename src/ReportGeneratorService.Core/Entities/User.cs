using ReportGeneratorService.Core.Interfaces.Services;
using ReportGeneratorService.Core.ValueObjects;

namespace ReportGeneratorService.Core.Entities;

public class User : Entity<Guid>
{
    public string Email { get; private set; } = null!;
    public string PasswordHash { get; private set; } = null!;
    public string MfaSecret { get; private set; } = null!;

    private readonly List<ContactInfo> _contactInfos = new();
    public IReadOnlyCollection<ContactInfo> ContactInfos => _contactInfos.AsReadOnly();

    private User() { } // For EF Core

    private User(string email, string passwordHash, string mfaSecret)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        MfaSecret = mfaSecret;
    }

    public static User Create(string email, string password, IMfaService mfaService,
        IPasswordHasher passwordHasher)
    {
        if (string.IsNullOrWhiteSpace(email))
            throw new ArgumentException("Email cannot be empty", nameof(email));
        
        if(string.IsNullOrWhiteSpace(password))
            throw new ArgumentException("Password cannot be empty", nameof(password));
        
        var passwordHash = passwordHasher.Hash(password);
        var mfaSecret = mfaService.GenerateSecret();
        
        return new User(email.Trim().ToLower(), passwordHash, mfaSecret);
    }

    public bool VerifyPassword(string password, IPasswordHasher passwordHasher)
    {
        return passwordHasher.Verify(PasswordHash, password);
    }

    public bool VerifyMfaCode(string code, IMfaService mfaService)
    {
        return mfaService.ValidateCode(MfaSecret, code);
    }

    public void UpdatePassword(string newPassword, IPasswordHasher passwordHasher)
    {
        PasswordHash = passwordHasher.Hash(newPassword);
        // Вместо прямого доступа к UpdatedAt, используем метод базового класса
        UpdateTimestamps();
    }
}