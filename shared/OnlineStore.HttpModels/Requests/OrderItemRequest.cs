using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class OrderItemRequest
{
    [Required] public Guid ProductId { get; set; }
    [Required] public int Quantity { get; set; }
    [Required] public decimal Price { get; set; }
}