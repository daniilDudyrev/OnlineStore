using Microsoft.EntityFrameworkCore;
using OnlineStore.Models;

namespace OnlineStore.Data.Repositories;

public class EfRepository<TEntity> : IRepository<TEntity> where TEntity : class, IEntity
{
    protected readonly AppDbContext DbContext;

    public EfRepository(AppDbContext dbContext)
    {
        DbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    protected DbSet<TEntity> Entities => DbContext.Set<TEntity>();

    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cts = default)
    {
        var product = await Entities.FirstAsync(it => it.Id == id, cts);
        return product;
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cts = default)
    {
        var products = await Entities.ToListAsync(cts);
        return products;
    }

    public virtual async Task Add(TEntity entity, CancellationToken cts = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await Entities.AddAsync(entity, cts);
        await DbContext.SaveChangesAsync(cts);
    }

    public virtual async Task Update(TEntity entity, CancellationToken cts = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        DbContext.Entry(entity).State = EntityState.Modified;
        await DbContext.SaveChangesAsync(cts);
    }

    public virtual async Task DeleteById(Guid id, CancellationToken cts = default)
    {
        var delEntity = await Entities.FirstAsync(it => it.Id == id, cts);
        Entities.Remove(delEntity);
        await DbContext.SaveChangesAsync(cts);
    }
}