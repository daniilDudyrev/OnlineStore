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

    public virtual async Task<Cart> GetCartForAccount(Guid accountId) =>
        await _unitOfWork.CartRepository.GetByAccountId(accountId);

    public virtual async Task<IReadOnlyCollection<CartItem>> GetItemsInCart(Guid accountId)
    {
        var cart = await _unitOfWork.CartRepository.GetByAccountId(accountId);
        var cartItems = cart.Items;
        return cartItems;
    }

    public virtual async Task AddItem(Guid accountId, Guid productId, int quantity = 1)
    {
        var cart = await _unitOfWork.CartRepository.GetByAccountId(accountId);
        cart.Add(productId, quantity);
        await _unitOfWork.CartRepository.Update(cart);
        await _unitOfWork.SaveChangesAsync();
    }
}