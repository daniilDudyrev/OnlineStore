namespace OnlineStore.Models;

public record Account: IEntity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public Guid Id { get; init; }

    public Account()
    {
    }

    public Account(Guid id, string name, string email)
    {
        Name = name;
        Email = email;
        Id = id;
    }
}