using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cts = default);
}