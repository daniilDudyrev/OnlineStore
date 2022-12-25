using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Services;
using OnlineStore.Models.Responses;
using OnlineStore.WebApi.Extensions;
using OnlineStore.WebApi.Mappers;

namespace OnlineStore.WebApi.Controllers;

[Route("orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;
    private readonly HttpModelsMapper _mapper;


    public OrderController(OrderService orderService, HttpModelsMapper mapper)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [Authorize]
    [HttpGet("get")]
    public async Task<ActionResult<OrderResponse>> GetOrder(CancellationToken cts)
    {
        var order = await _orderService.GetOrderForAccount(User.GetAccountId(), cts);
        return new OrderResponse(order.Items.Select(_mapper.MapOrderItemModel), order.Id, order.AccountId, order.OrderDate);
    }

    [Authorize]
    [HttpPost("create_order")]
    public async Task<ActionResult<OrderResponse>> CreateOrder(CancellationToken cts)
    {
        var accountId = User.GetAccountId();
        var order = await _orderService.CreateOrder(accountId, cts);
        return new OrderResponse(order.Items.Select(_mapper.MapOrderItemModel), order.Id, order.AccountId,
            order.OrderDate);
    }
}