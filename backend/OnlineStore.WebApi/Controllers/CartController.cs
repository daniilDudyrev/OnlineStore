using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Services;
using OnlineStore.Models.Responses;
using OnlineStore.WebApi.Extensions;
using OnlineStore.WebApi.Mappers;

namespace OnlineStore.WebApi.Controllers;

[ApiController]
[Route("cart")]
public class CartController : ControllerBase
{
    private readonly CartService _cartService;
    private readonly HttpModelsMapper _mapper;

    public CartController(CartService cartService, HttpModelsMapper mapper)
    {
        _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [Authorize]
    [HttpGet("get")]
    public async Task<ActionResult<CartResponse>> GetCart()
    {
        var cart = await _cartService.GetCartForAccount(User.GetAccountId());
        return new CartResponse(cart.Items.Select(_mapper.MapCartItemModel), cart.Id, cart.AccountId, cart.ItemCount);
    }

    [Authorize]
    [HttpPost("add_item")]
    public async Task<ActionResult<CartItemResponse>> AddItem(Guid productId, int quantity = 1)
    {
        var accountId = User.GetAccountId();
        await _cartService.AddItem(accountId, productId, 1);
        return new CartItemResponse(productId, quantity);
    }
}