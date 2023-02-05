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
    public async Task<ActionResult<CartResponse>> GetCart(CancellationToken cancellationToken)
    {
        var cart = await _cartService.GetCartForAccount(User.GetAccountId(), cancellationToken);
        return new CartResponse(cart.Items.Select(_mapper.MapCartItemModel), cart.Id, cart.AccountId, cart.ItemCount);
    }

    [Authorize]
    [HttpPost("add_item")]
    public async Task<ActionResult<CartItemResponse>> AddItem(Guid productId, CancellationToken cancellationToken, int quantity)
    {
        var accountId = User.GetAccountId();
        var cartItem = await _cartService.AddItem(accountId, productId, cancellationToken, quantity);
        return new CartItemResponse(cartItem.Id, productId, quantity);
    }

    [Authorize]
    [HttpDelete("delete_item")]
    public async Task<ActionResult<CartItemResponse>> DeleteItem(Guid id, int quantity, CancellationToken cancellationToken)
    {
        var accountId = User.GetAccountId();
        var cartItem = await _cartService.DeleteItem(id, accountId, quantity, cancellationToken);
        return new CartItemResponse(cartItem.Id, cartItem.ProductId, quantity);
    }

    [Authorize]
    [HttpDelete("clear")]
    public async Task<ActionResult<CartResponse>> ClearCart(CancellationToken cancellationToken)
    {
        var accountId = User.GetAccountId();
        var cart = await _cartService.ClearCart(accountId, cancellationToken);
        return new CartResponse(cart.Items.Select(_mapper.MapCartItemModel), cart.Id, cart.AccountId, cart.ItemCount);
    }
}