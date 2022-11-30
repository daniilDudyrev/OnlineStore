using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data.Repositories;

public class AccountRepository : EfRepository<Account>, IAccountRepository
{
    public AccountRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Account> GetByEmail(string email, CancellationToken cancellationToken = default) =>
        await Entities.FirstAsync(it => it.Email == email, cancellationToken);
}