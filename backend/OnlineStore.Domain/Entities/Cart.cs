namespace OnlineStore.Domain.Entities;

public record Cart : IEntity
{
    protected Cart()
    {
        _items = new List<CartItem>();
    }

    public Cart(Guid id, Guid accountId, List<CartItem> items)
    {
        Id = id;
        AccountId = accountId;
        _items = items;
    }

    public Guid Id { get; init; }
    public Guid AccountId { get; set; }

    private readonly List<CartItem> _items;
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public int ItemCount => Items.Count;

    public CartItem Add(Product product, int quantity)
    {
        var cartItem = Items.SingleOrDefault(it => it.ProductId == product.Id);
        if (cartItem is not null)
        {
            var newQty = cartItem.Quantity + quantity;
            if (newQty > 1000)
            {
                throw new InvalidOperationException("Quantity cannot be greater than 1000");
            }

            cartItem.Quantity = newQty;
        }
        else
        {
            cartItem = new CartItem(Guid.Empty, product.Id, quantity, product.Price);
            _items.Add(cartItem);
        }

        return cartItem;
    }

    public void Clear()
    {
        _items.Clear();
    }

    public void DeleteItem(Guid id)
    {
        _items.Remove(_items.SingleOrDefault(it => it.Id == id)!);
    }
}