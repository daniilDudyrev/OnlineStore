using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IProductRepository : IRepository<Product>
{
    Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cancellationToken = default);
    Task<Product> GetByName(string name, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Product>> GetProductsByCategoryId(Guid categoryId, CancellationToken cancellationToken = default);
}