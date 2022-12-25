namespace OnlineStore.Models.Responses;

public record ProductResponse(Guid ProductId, string ProductName, decimal Price, string Image, string Description,
    Guid CategoryId);