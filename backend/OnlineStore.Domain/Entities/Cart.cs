namespace OnlineStore.Domain.Entities;

public class Cart : IEntity
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

    private List<CartItem> _items;
    public IReadOnlyCollection<CartItem> Items => _items.AsReadOnly();

    public int ItemCount => Items.Count;

    public void Add(Guid productId, int quantity)
    {
        var cartItem = Items.SingleOrDefault(it => it.ProductId == productId);
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
            cartItem = new CartItem(Guid.Empty, productId, quantity);
            _items.Add(cartItem);
        }
    }
}