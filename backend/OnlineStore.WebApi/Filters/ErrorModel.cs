namespace OnlineStore.WebApi.Filters;

public record ErrorModel(string Message)
{
    public override string ToString()
    {
        return $"{{Message = {Message}}}";
    }
}