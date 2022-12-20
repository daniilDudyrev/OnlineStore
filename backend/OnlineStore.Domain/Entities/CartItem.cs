namespace OnlineStore.Domain.Entities;

public record CartItem : IEntity
{
    protected CartItem()
    {
    }

    public CartItem(Guid id, Guid productId, int quantity)
    {
        Id = id;
        ProductId = productId;
        Quantity = quantity;
    }

    public Guid Id { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; set; }
    public Guid CartId { get; set; }
    public Cart Cart { get; set; } = null!;
}