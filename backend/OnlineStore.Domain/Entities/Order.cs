namespace OnlineStore.Domain.Entities;

public record Order : IEntity
{
    protected Order()
    {
        _items = new List<OrderItem>();
    }

    public Order(Guid id, Guid accountId, List<OrderItem> items)
    {
        Id = id;
        AccountId = accountId;
        _items = items;
    }

    public Guid Id { get; init; }
    public Guid AccountId { get; set; }
    public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
    private readonly List<OrderItem> _items;
    public IReadOnlyCollection<OrderItem> Items => _items.AsReadOnly();
    
    public decimal GetTotalPrice()
    {
        var totalPrice = _items.Sum(item => item.Price * item.Quantity);
        return totalPrice;
    }
    
    public void Add(OrderItem orderItem)
    {
        var existedItem = Items.SingleOrDefault(it => it.ProductId == orderItem.ProductId);
        if (existedItem is not null)
        {
            var newQty = existedItem.Quantity + orderItem.Quantity;
            if (newQty > 1000)
            {
                throw new InvalidOperationException("Quantity cannot be greater than 1000");
            }
    
            existedItem.Quantity = newQty;
        }
        else
        {
            _items.Add(orderItem);
        }
    }
}