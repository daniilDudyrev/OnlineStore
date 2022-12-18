namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IRepository<TEntity>
{
    Task<TEntity> GetById(Guid id, CancellationToken cts = default);
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cts = default);
    Task Add(TEntity entity, CancellationToken cts = default);
    Task Update(TEntity entity, CancellationToken cts = default);
    Task<TEntity> DeleteById(Guid id, CancellationToken cts = default);
}