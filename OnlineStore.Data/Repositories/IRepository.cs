namespace OnlineStore.Data.Repositories;

public interface IRepository<TEntity>
{
    Task<TEntity> GetById(Guid id,CancellationToken cts = default);
    Task<IReadOnlyList<TEntity>> GetAll(CancellationToken cts = default);
    Task Add(TEntity entity,CancellationToken cts = default);
    Task Update(TEntity entity,CancellationToken cts = default);
    Task DeleteById(Guid id, CancellationToken cts = default);
}