using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class OrderRequest
{
    [Required] public IReadOnlyList<OrderItemRequest> Items { get; set; }
    [Required] public Guid OrderId { get; set; }
    [Required] public Guid AccountId { get; set; }
    [Required] public DateTimeOffset OrderDate { get; set; }
}