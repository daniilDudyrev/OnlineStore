using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetByAccountId(Guid accountId,CancellationToken cts = default);
    Task<Cart?> FindByAccountId(Guid accountId,CancellationToken cts = default);
}