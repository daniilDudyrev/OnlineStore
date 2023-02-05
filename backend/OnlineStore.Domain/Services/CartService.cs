using System.Reflection.Metadata;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Domain.Services;

public class CartService
{
    private readonly IUnitOfWork _unitOfWork;

    public CartService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public virtual async Task<Cart> GetCartForAccount(Guid accountId, CancellationToken cancellationToken) =>
        await _unitOfWork.CartRepository.GetByAccountId(accountId, cancellationToken);

    public virtual async Task<IReadOnlyCollection<CartItem>> GetItemsInCart(Guid accountId, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetByAccountId(accountId, cancellationToken);
        var cartItems = cart.Items;
        return cartItems;
    }

    public virtual async Task<CartItem> AddItem(Guid accountId, Guid productId, CancellationToken cancellationToken, int quantity)
    {
        var cart = await _unitOfWork.CartRepository.GetByAccountId(accountId, cancellationToken);
        var product = await _unitOfWork.ProductRepository.GetById(productId, cancellationToken);
        var cartItem = cart.Add(product, quantity);
        await _unitOfWork.CartRepository.Update(cart, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return cartItem;
    }

    public virtual async Task<CartItem> DeleteItem(Guid id, Guid accountId, int quantity, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetByAccountId(accountId, cancellationToken);
        var cartItem = await _unitOfWork.CartRepository.GetItemById(id, accountId, cancellationToken);
        cartItem.Quantity -= quantity;
        if (cartItem.Quantity == 0)
        {
            cart.DeleteItem(id);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return cartItem;
    }


    public virtual async Task<Cart> ClearCart(Guid accountId, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetByAccountId(accountId, cancellationToken);
        cart.Clear();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return cart;
    }
}