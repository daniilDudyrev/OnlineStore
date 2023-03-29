using OnlineStore.Domain.Entities;

namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IParentCategoryRepository : IRepository<ParentCategory>
{
    Task<ParentCategory> GetByName(string name, CancellationToken cancellationToken = default);
}