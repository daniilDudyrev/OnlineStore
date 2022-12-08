namespace OnlineStore.Domain.Entities;

public record Account : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }

#pragma warning disable CS8618
    private Account()
#pragma warning restore CS8618
    {
    }

    public Account(Guid id, string name, string email, string password)
    {
        Id = id;
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Email = email ?? throw new ArgumentNullException(nameof(email));
        Password = password ?? throw new ArgumentNullException(nameof(password));
    }
}