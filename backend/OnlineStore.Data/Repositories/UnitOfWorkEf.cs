using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Data.Repositories;

public class UnitOfWorkEf : IUnitOfWork, IDisposable
{
    private readonly AppDbContext _dbContext;
    public IAccountRepository AccountRepository { get; }
    public ICartRepository CartRepository { get; }
    public IProductRepository ProductRepository { get; }

    public UnitOfWorkEf(AppDbContext dbContext,IAccountRepository accountRepository, ICartRepository cartRepository, IProductRepository productRepository)
    {
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        AccountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
        CartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
        ProductRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
        => _dbContext.SaveChangesAsync(cancellationToken);

    public void Dispose() => _dbContext.Dispose();
    public ValueTask DisposeAsync() => _dbContext.DisposeAsync();
}