using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface ICategoryRepository : IRepository<Category>
{
    Task<Category> GetByName(string name, CancellationToken cancellationToken = default);
}