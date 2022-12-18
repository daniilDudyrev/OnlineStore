using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Data.Repositories;

public class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Cart> GetByAccountId(Guid accountId, CancellationToken cts = default)
    {
        var cart = await Entities
                       .SingleOrDefaultAsync(it => it.AccountId == accountId, cts)
                   ?? Entities.Local.Single(it => it.AccountId == accountId);
        return cart;
    }

    public async Task<Cart?> FindByAccountId(Guid accountId, CancellationToken cts = default)
    {
        var cart = await Entities.FirstOrDefaultAsync(
                   it => it.AccountId == accountId, cts)
               ?? Entities.Local.FirstOrDefault(it => it.AccountId == accountId);
        return cart;
    }
}