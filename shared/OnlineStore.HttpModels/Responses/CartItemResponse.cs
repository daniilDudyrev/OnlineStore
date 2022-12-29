namespace OnlineStore.Models.Responses;

public record CartItemResponse(Guid Id, Guid ProductId, int Quantity);