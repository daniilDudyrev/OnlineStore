using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IOrderRepository : IRepository<Order>
{
    Task<Order> GetByAccountId(Guid accountId,CancellationToken cancellationToken = default);
    Task<Order?> FindByAccountId(Guid accountId,CancellationToken cancellationToken = default);
}