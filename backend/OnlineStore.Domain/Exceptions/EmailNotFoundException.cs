namespace OnlineStore.Domain.Exceptions;

public class EmailNotFoundException : Exception
{
    public EmailNotFoundException(string message) : base(message)
    {
    }
}