using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetByAccountId(Guid accountId,CancellationToken cts = default);
    Task<Order?> FindByAccountId(Guid accountId,CancellationToken cts = default);
}