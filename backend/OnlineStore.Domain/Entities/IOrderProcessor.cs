namespace OnlineStore.Domain.Entities;

public interface IOrderProcessor
{
    void ProcessOrder(Order order, ShippingDetails shippingDetails, CancellationToken cancellationToken = default);
}