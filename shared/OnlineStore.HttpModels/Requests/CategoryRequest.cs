using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class CategoryRequest
{
    [Required] public string Name { get; set; }
}