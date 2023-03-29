using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Data.Repositories;

public class ParentCategoryRepository : EfRepository<ParentCategory>, IParentCategoryRepository
{
    public ParentCategoryRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null) throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<ParentCategory> GetByName(string name, CancellationToken cancellationToken = default)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        return Entities.FirstAsync(it => it.Name == name, cancellationToken);
    }
}