using System.ComponentModel.DataAnnotations;

namespace OnlineStore.Models.Requests;

public class ParentCategoryRequest
{
    [Required] public string Name { get; set; }
}