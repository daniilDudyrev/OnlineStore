using Microsoft.EntityFrameworkCore;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Exceptions;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Data.Repositories;

public class CartRepository : EfRepository<Cart>, ICartRepository
{
    public CartRepository(AppDbContext dbContext) : base(dbContext)
    {
        if (dbContext == null)
        {
            throw new ArgumentNullException(nameof(dbContext));
        }
    }

    public async Task<Cart> GetByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        var cart = await Entities
                       .SingleOrDefaultAsync(it => it.AccountId == accountId, cancellationToken)
                   ?? Entities.Local.Single(it => it.AccountId == accountId);
        return cart;
    }

    public async Task<Cart?> FindByAccountId(Guid accountId, CancellationToken cancellationToken = default)
    {
        var cart = await Entities.FirstOrDefaultAsync(
                       it => it.AccountId == accountId, cancellationToken)
                   ?? Entities.Local.FirstOrDefault(it => it.AccountId == accountId);
        return cart;
    }

    public async Task<CartItem> GetItemById(Guid id, Guid accountId, CancellationToken cancellationToken = default)
    {
        var cart = await GetByAccountId(accountId, cancellationToken);
        var cartItem = cart.Items.SingleOrDefault(it => it.Id == id);
        if (cartItem == null)
        {
            throw new NoSuchItemCartException($"There is no such item with {id} in Cart");
        }

        return cartItem;
    }
}