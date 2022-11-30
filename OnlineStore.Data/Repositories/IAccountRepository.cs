using OnlineStore.Models;

namespace OnlineStore.Data.Repositories;


public interface IAccountRepository: IRepository<Account>
{
    public Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default);
}