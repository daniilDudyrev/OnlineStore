namespace OnlineStore.Domain.Entities;

public class CartItem : IEntity
{
    protected CartItem()
    {
    }
    
    public CartItem(Guid id, Guid productId, decimal price, int quantity)
    {
        Id = id;
        ProductId = productId;
        Price = price;
        Quantity = quantity;
    }
    
    public Guid Id { get; init; }
    public Guid ProductId { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }
    public Cart Cart { get; set; } = null!;
}