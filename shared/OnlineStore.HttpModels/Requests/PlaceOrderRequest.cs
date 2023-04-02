using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class PlaceOrderRequest
{
    [Required] public IReadOnlyList<OrderItemRequest> Items { get; set; }
    [Required] public Guid OrderId { get; set; }
    [Required] public Guid AccountId { get; set; }
    [Required] public DateTimeOffset OrderDate { get; set; }
    [Required] public string City { get; set; }
    [Required] public string Address { get; set; }
}