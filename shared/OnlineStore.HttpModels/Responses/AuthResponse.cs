namespace OnlineStore.Models.Responses;

public record AuthResponse(Guid AccountId, string AccountName, string Email, string Token);