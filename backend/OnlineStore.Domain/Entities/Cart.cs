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
    public int ItemsCount => Items.Count;
    
    public void Add(Guid productId,decimal price, int quantity = 1)
    {
        var cartItem = Items.SingleOrDefault(it => it.ProductId == productId);
        if (cartItem != null)
        {
            var newQuantity = cartItem.Quantity + quantity;
        }
        else
        {
            cartItem = new CartItem(Guid.Empty, productId, price , quantity);
            _items.Add(cartItem);
        }
    }
}