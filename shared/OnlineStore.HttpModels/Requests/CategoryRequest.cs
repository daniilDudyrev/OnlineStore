using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class CategoryRequest
{
    //[Required] public Guid CategoryId { get; set; }
    [Required] public Guid ParentId { get; set; }
    [Required] public string Name { get; set; }
}