using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class ProductRequest
{
    [Required] public string Name { get; set; }
    [Required] public decimal Price { get; set; }
    [Required] public string Image { get; set; }
    [Required] public string Description { get; set; }
    [Required] public Guid CategoryId { get; set; }
}