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

    public virtual async Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await Entities.FirstAsync(it => it.Id == id, cancellationToken);
        return entity;
    }

    public virtual async Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default)
    {
        var entities = await Entities.ToListAsync(cancellationToken);
        return entities;
    }

    public virtual async Task Add(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        await Entities.AddAsync(entity, cancellationToken);
    }

    public virtual ValueTask Update(TEntity entity, CancellationToken cancellationToken = default)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }
        DbContext.Update(entity);
        return ValueTask.CompletedTask;
    }

    public virtual async Task<TEntity> DeleteById(Guid id, CancellationToken cancellationToken = default)
    {
        var delEntity = await Entities.FirstAsync(it => it.Id == id, cancellationToken);
        Entities.Remove(delEntity);
        return delEntity;
    }
}