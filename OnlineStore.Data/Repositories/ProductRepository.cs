using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data.Repositories;

public class ProductRepository : EfRepository<Product>,IProductRepository
{
    public ProductRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }
    
    public async Task<IReadOnlyList<Product>> FindByName(string name, CancellationToken cts = default)
    {
        if (name == null) throw new ArgumentNullException(nameof(name));
        var products = await Entities
            .Where(it => it.Name.Contains(name))
            .ToListAsync(cts);
        return products;
    }
}