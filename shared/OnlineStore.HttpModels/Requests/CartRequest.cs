using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class CartRequest
{
    [Required] public IReadOnlyList<CartItemRequest> Items { get; set; }
    [Required] public Guid CartId { get; set; }
    [Required] public Guid AccountId { get; set; }
    [Required] public int ItemsCount { get; set; }
}