using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Domain.Services;

public class OrderService
{
    private readonly IUnitOfWork _unitOfWork;

    public OrderService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public virtual async Task<Order> GetOrderForAccount(Guid accountId, CancellationToken cts) =>
        await _unitOfWork.OrderRepository.GetByAccountId(accountId, cts);

    public virtual async Task<Order> CreateOrder(Guid accountId, CancellationToken cts)
    {
        var cart = await _unitOfWork.CartRepository.GetByAccountId(accountId, cts);
        var order = new Order(Guid.NewGuid(), accountId, new List<OrderItem>());
        var items = cart.Items.Select(it =>
        {
            var orderItem = new OrderItem(Guid.NewGuid(), it.ProductId, it.Quantity, it.Price);
            order.Add(orderItem);
            return orderItem;
        }).ToList();
        
        await _unitOfWork.OrderRepository.Add(order, cts);
        await _unitOfWork.SaveChangesAsync(cts);
        return order;
    }
}