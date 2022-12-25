namespace OnlineStore.Models.Responses;

public record OrderResponse(IEnumerable<OrderItemResponse> Items, Guid OrderId, Guid AccountId,
    DateTimeOffset OrderDate);