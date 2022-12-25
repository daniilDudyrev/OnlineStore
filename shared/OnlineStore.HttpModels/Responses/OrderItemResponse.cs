namespace OnlineStore.Models.Responses;

public record OrderItemResponse(Guid ProductId, int Quantity, decimal Price);