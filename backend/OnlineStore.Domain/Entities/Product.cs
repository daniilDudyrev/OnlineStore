#pragma warning disable CS8618
namespace OnlineStore.Domain.Entities;

public record Product : IEntity
{
    public Guid Id { get; init; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public string Image { get; set; }
    public string Description { get; set; }
    public Guid CategoryId { get; set; }


    public Product()
    {
    }

    public Product(Guid id, string name, decimal price, string image,string description, Guid categoryId)
    {
        Name = name ?? throw new ArgumentNullException(nameof(name));
        Price = price;
        Id = id;
        Image = image;
        Description = description;
        CategoryId = categoryId;
    }
}