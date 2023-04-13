using System.Text;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Domain.Services;

public class OrderService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IEmailSender _emailSender;

    public OrderService(IUnitOfWork unitOfWork, IEmailSender emailSender)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
        _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));

    }

    public virtual async Task<Order> GetOrderForAccount(Guid accountId, CancellationToken cancellationToken) =>
        await _unitOfWork.OrderRepository.GetByAccountId(accountId, cancellationToken);

    public virtual async Task<Order> PlaceOrderAndCreateNew(Guid accountId, string city, string address, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetByAccountId(accountId, cancellationToken);
        var order = new Order(Guid.NewGuid(), accountId, new List<OrderItem>());
        foreach (var item in cart.Items)
        {
            var orderItem = new OrderItem(Guid.NewGuid(), item.ProductId, item.Quantity, item.Price);
            order.Add(orderItem);
        }

        await _unitOfWork.OrderRepository.Add(order, cancellationToken);
        cart.Clear();
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return order;
    }

    public async Task NotifyAboutPlacedOrder(Order order, string city, string address, CancellationToken cancellationToken = default)
    {
        StringBuilder body = new StringBuilder()
            .AppendLine("Новый заказ обработан")
            .AppendLine("---")
            .AppendLine("Товары:");

        foreach (var line in order.Items)
        {
            var subtotal = line.Price * line.Quantity;
            body.AppendFormat("{0} x {1} (Итого: {2:c}",
                line.Price, line.Quantity, subtotal);
        }

        
        body.AppendFormat("Общая стоимость: {0:c}", order.GetTotalPrice())
            .AppendLine("---")
            .AppendLine("Доставка:")
            .AppendLine(address)
            .AppendLine(city)
            .AppendLine("---");

        await _emailSender.Send(ShopConfig.ManagerEmail, "Новый заказ отправлен", body.ToString(), cancellationToken);
    }
}

public class ShopConfig
{
    public static string ManagerEmail { get; } = "manager@aridon.com";
}
