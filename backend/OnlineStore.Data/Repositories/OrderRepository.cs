using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Data.Repositories;

public class OrderRepository : EfRepository<Order>, IOrderRepository
{
    public OrderRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }
    }

    public async Task<Order> GetByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        var order = await Entities
                        .SingleOrDefaultAsync(it => it.AccountId == accountId, cancellationToken)
                    ?? Entities.Local.Single(it => it.AccountId == accountId);
        return order;
    }

    public async Task<Order?> FindByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        var order = await Entities.FirstOrDefaultAsync(
                        it => it.AccountId == accountId, cancellationToken)
                    ?? Entities.Local.FirstOrDefault(it => it.AccountId == accountId);
        return order;
    }
}