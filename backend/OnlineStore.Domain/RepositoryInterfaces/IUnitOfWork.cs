namespace OnlineStore.Domain.RepositoryInterfaces;

public interface IUnitOfWork
{
    IAccountRepository AccountRepository { get; }
    ICartRepository CartRepository { get; }
    IProductRepository ProductRepository { get; }
    IParentCategoryRepository ParentCategoryRepository { get; }

    ICategoryRepository CategoryRepository { get; }
    IOrderRepository OrderRepository { get; }
    Task SaveChangesAsync(CancellationToken cancellationToken = default);
}