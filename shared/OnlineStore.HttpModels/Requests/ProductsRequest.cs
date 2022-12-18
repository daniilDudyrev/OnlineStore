using OnlineStore.Models.Responses;

namespace OnlineStore.Models.Requests;

public class ProductsRequest
{
    public IEnumerable<ProductResponse> Products { get; set; }
}