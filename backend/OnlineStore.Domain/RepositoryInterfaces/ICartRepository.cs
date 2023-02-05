using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface ICartRepository : IRepository<Cart>
{
    Task<Cart> GetByAccountId(Guid accountId, CancellationToken cancellationToken = default);
    Task<Cart?> FindByAccountId(Guid accountId, CancellationToken cancellationToken = default);
    Task<CartItem> GetItemById(Guid id, Guid accountId, CancellationToken cancellationToken = default);
}