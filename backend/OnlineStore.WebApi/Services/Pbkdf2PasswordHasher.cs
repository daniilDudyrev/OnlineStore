using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;

namespace OnlineStore.WebApi.Services;

public class Pbkdf2PasswordHasher : IPasswordHasherService
{
    private readonly PasswordHasher<Account> _hasher;
    private readonly Account _account = new(Guid.Empty, "", "fake@fake.com", "");

    public Pbkdf2PasswordHasher(IOptions<PasswordHasherOptions> optionsAccessor)
    {
        _hasher = new PasswordHasher<Account>(optionsAccessor);
    }

    public string HashPassword(string password)
    {
        var hashedPassword = _hasher.HashPassword(_account, password);
        return hashedPassword;
    }

    public bool VerifyPassword(string passwordHash, string password)
    {
        var result = _hasher.VerifyHashedPassword(_account, passwordHash, password);
        return result is not PasswordVerificationResult.Failed;
    }
}