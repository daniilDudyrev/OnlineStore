namespace OnlineStore.Models.Responses;

public record RegisterResponse(Guid AccountId, string AccountName, string Email, string Token);