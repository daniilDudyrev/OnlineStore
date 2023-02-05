using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Domain.Services;

public class ProductService
{
    private readonly IUnitOfWork _unitOfWork;

    public ProductService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public virtual async Task<IEnumerable<Product>> GetProducts(CancellationToken cancellationToken) =>
        await _unitOfWork.ProductRepository.GetAll(cancellationToken);

    public virtual async Task<IEnumerable<Product>> GetProductsByCategoryId(Guid categoryId, CancellationToken cancellationToken) =>
        await _unitOfWork.ProductRepository.GetProductsByCategoryId(categoryId, cancellationToken);

    public virtual async Task<Product> GetProduct(Guid id, CancellationToken cancellationToken) =>
        await _unitOfWork.ProductRepository.GetById(id, cancellationToken);

    public virtual async Task<Product> AddProduct(string name, decimal price, string image, string description,
        Guid categoryId, CancellationToken cancellationToken)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var product = new Product(Guid.NewGuid(), name, price, image, description, categoryId);
        await _unitOfWork.ProductRepository.Add(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product;
    }

    public virtual async Task<Product> UpdateProduct(string name, decimal price, string image, string description,
        Guid categoryId, CancellationToken cancellationToken)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        if (image == null)
        {
            throw new ArgumentNullException(nameof(image));
        }

        if (description == null)
        {
            throw new ArgumentNullException(nameof(description));
        }

        var product = await _unitOfWork.ProductRepository.GetByName(name, cancellationToken);
        product.Name = name;
        product.Price = price;
        product.Image = image;
        product.Description = description;
        product.CategoryId = categoryId;
        await _unitOfWork.ProductRepository.Update(product, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product;
    }

    public virtual async Task<Product> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.DeleteById(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return product;
    }
}