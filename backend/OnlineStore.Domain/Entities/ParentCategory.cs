namespace OnlineStore.Domain.Entities;

public class ParentCategory : IEntity
{
    protected ParentCategory()
    {
    }

    public ParentCategory(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public Guid Id { get; init; }
    public string Name { get; set; }
}