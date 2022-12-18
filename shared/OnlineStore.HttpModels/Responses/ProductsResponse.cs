namespace OnlineStore.Models.Responses;

public record ProductsResponse(IEnumerable<ProductResponse> Products);