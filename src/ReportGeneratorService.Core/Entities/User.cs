using ReportGeneratorService.Core.Interfaces.Services;

namespace ReportGeneratorService.Core.Entities;

public class User : Entity<Guid>
{
    public string Email { get; private set; }
    public string PasswordHash { get; private set; }
    public string MfaSecret { get; private set; }
    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private User() { }

    private User(string email, string passwordHash, string mfaSecret)
    {
        Id = Guid.NewGuid();
        Email = email;
        PasswordHash = passwordHash;
        MfaSecret = mfaSecret;
        CreatedAt = DateTime.UtcNow;
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
        UpdatedAt = DateTime.UtcNow;
    }
    
}