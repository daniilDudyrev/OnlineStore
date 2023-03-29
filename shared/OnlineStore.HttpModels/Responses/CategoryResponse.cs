namespace OnlineStore.Models.Responses;

public record CategoryResponse(Guid ParentId, Guid CategoryId, string CategoryName);