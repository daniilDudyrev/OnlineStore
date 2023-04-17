namespace OnlineStore.Domain.Entities;

public class Category : IEntity
{
    protected Category()
    {
    }

    public Category(Guid id, Guid parentId, string name)
    {
        Id = id;
        ParentId = parentId;
        Name = name;
    }
    
    public Guid Id { get; init; }
    public string Name { get; set; }
    public Guid ParentId { get; set; }
}