using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;
using OnlineStore.WebApi.Extensions;

namespace OnlineStore.WebApi.Controllers;

[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;

    public CartController(CartService cartService)
    {
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
    }

    [Authorize]
    [HttpGet("get")]
    public async Task<ActionResult<Cart>> GetCart()
    {
        return await _cartService.GetCartForAccount(User.GetAccountId());
    }

    [Authorize]
    [HttpPost("add_item")]
    public async Task<ActionResult<Cart>> AddItem(Product product)
    {
        if (product == null)
        {
            throw new ArgumentNullException(nameof(product));
        }

        var accountId = User.GetAccountId();
        return await _cartService.AddItem(accountId, product.Id, product.Price);
    }

    [Authorize]
    [HttpPost("get_items")]
    public async Task<ActionResult<IEnumerable<CartItem>>> GetItemsInCart()
    {
        var accountId = User.GetAccountId();
        var items =  await _cartService.GetItemsInCart(accountId);
        return items.ToList();
    }

}