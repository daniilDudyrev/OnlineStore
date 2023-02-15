namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IRepository<TEntity>
{
    Task<TEntity> GetById(Guid id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cancellationToken = default);
    Task Add(TEntity entity, CancellationToken cancellationToken = default);
    ValueTask Update(TEntity entity, CancellationToken cancellationToken = default);
    Task<TEntity> DeleteById(Guid id, CancellationToken cancellationToken = default);
}