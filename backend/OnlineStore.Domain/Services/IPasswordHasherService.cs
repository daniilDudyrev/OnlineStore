namespace OnlineStore.Domain.Services;

public interface IPasswordHasherService
{
    string HashPassword(string password);
    bool VerifyPassword(string passwordHash, string password);
}