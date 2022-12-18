namespace OnlineStore.Domain.Entities;

public class Order : IEntity
{
    public Guid Id { get; init; }
    public decimal TotalPrice { get; set; }
    public OrderStatus Status { get; set; }

    public enum OrderStatus
    {
        Created,
        Offered
    }
}