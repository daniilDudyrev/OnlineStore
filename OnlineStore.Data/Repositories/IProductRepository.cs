using OnlineStore.Models;

namespace OnlineStore.Data.Repositories;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cts = default);
}