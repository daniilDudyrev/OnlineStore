using OnlineStore.Models.Responses;

namespace OnlineStore.Models.Requests;

public class ParentCategoriesRequest
{
    public IEnumerable<ParentCategoryResponse> Categories { get; set; }
}