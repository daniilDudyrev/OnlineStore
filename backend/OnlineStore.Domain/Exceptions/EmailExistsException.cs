namespace OnlineStore.Domain.Exceptions;

[Serializable]
public class EmailExistsException : Exception
{
    public EmailExistsException(string message) : base(message)
    {
    }
}