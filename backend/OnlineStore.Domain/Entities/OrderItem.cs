namespace OnlineStore.Domain.Entities;

public record OrderItem : IEntity
{
    protected OrderItem()
    {
    }

    public OrderItem(Guid id, Guid productId, int quantity, decimal price)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
        Price = price;
    }

    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public Guid OrderId { get; set; }
    public Order Order { get; set; } = null!;
}