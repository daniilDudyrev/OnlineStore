using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Data.Repositories;

public class UnitOfWorkEf : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _dbContext;
    public IAccountRepository AccountRepository { get; }
    public ICartRepository CartRepository { get; }
    public IProductRepository ProductRepository { get; }
    public IParentCategoryRepository ParentCategoryRepository { get; }
    public ICategoryRepository CategoryRepository { get; }
    public IOrderRepository OrderRepository { get; }

    public UnitOfWorkEf(AppDbContext dbContext, IAccountRepository accountRepository, ICartRepository cartRepository,
        IProductRepository productRepository, ICategoryRepository categoryRepository, IOrderRepository orderRepository, IParentCategoryRepository parentCategoryRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        CartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        ProductRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        CategoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
        OrderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        ParentCategoryRepository = parentCategoryRepository ?? throw new ArgumentNullException(nameof(parentCategoryRepository));
    }

    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose() => _dbContext.Dispose();
    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
}