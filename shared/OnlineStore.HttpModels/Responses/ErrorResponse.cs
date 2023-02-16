namespace OnlineStore.Models.Responses;

public record ErrorResponse(string Message)
{
    public override string ToString()
    {
        return $"{{Message = {Message}}}";
    }
}