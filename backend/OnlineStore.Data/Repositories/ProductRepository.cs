using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Data.Repositories;

public class ProductRepository : EfRepository<Product>, IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<Product> GetByName(string name, CancellationToken cancellationToken = default)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return Entities.FirstAsync(it => it.Name == name, cancellationToken);
    }

    public async Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cancellationToken = default)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var products = await Entities
            .Where(it => it.Name.Contains(name))
            .ToListAsync(cancellationToken);
        return products;
    }

    public async Task<IReadOnlyList<Product>> GetProductsByCategoryId(Guid categoryId, CancellationToken cancellationToken = default)
    {
        var products = await Entities
            .Where(it => it.CategoryId == categoryId)
            .ToListAsync(cancellationToken);
        return products;
    }
}