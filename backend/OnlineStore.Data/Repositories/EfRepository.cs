using System.Collections;
using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

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
        var entity = await Entities.FirstAsync(it => it.Id == id, cts);
        return entity;
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cts = default)
    {
        var entities = await Entities.ToListAsync(cts);
        return entities;
    }

    public virtual async Task Add(TEntity entity, CancellationToken cts = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        await Entities.AddAsync(entity, cts);
        // await DbContext.SaveChangesAsync(cts);
    }

    public virtual async Task Update(TEntity entity, CancellationToken cts = default)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));
        // DbContext.Entry(entity).State = EntityState.Modified;
        Entities.Update(entity);
        
        // await DbContext.SaveChangesAsync(cts);
    }

    public virtual async Task<TEntity> DeleteById(Guid id, CancellationToken cts = default)
    {
        var delEntity = await Entities.FirstAsync(it => it.Id == id, cts);
        Entities.Remove(delEntity);
        // await DbContext.SaveChangesAsync(cts);
        return delEntity;
    }
}