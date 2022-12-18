using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.Services;

public interface ITokenService
{
    string GenerateToken(Account account);
}