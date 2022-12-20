namespace OnlineStore.Models.Responses;

public record CartResponse
    (IEnumerable<CartItemResponse> Items, Guid CartId, Guid AccountId, int ItemsCount);