using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IAccountRepository : IRepository<Account>
{
    public Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default);
    public Task<Account?> FindByEmail(string email, CancellationToken cancellationToken = default);
}