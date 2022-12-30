using OnlineStore.Models.Responses;

namespace OnlineStore.Models.Requests;

public class CategoriesRequest
{
    public IEnumerable<CategoryResponse> Categories { get; set; }
}