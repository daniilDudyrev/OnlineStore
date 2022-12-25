namespace OnlineStore.Domain.Entities;

public class Category : IEntity
{
    protected Category()
    {
    }

    public Category(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
    
    public Guid Id { get; init; }
    public string Name { get; set; }
}