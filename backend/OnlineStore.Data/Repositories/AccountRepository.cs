using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Data.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }
    }

    public Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email));
        }
        return Entities.FirstAsync(it => it.Email == email, cancellationToken);
    }

    public Task<Account?> FindByEmail(string email, CancellationToken cancellationToken = default)
    {
        if (email == null)
        {
            throw new ArgumentNullException(nameof(email));
        }
        return Entities.FirstOrDefaultAsync(it => it.Email == email, cancellationToken);
    }
}