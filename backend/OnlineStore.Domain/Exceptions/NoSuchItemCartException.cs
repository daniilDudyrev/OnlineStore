namespace OnlineStore.Domain.Exceptions;

public class NoSuchItemCartException : Exception
{
    public NoSuchItemCartException(string message) : base(message)
    {
    }
}