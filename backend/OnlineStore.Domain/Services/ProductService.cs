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

    public virtual async Task<IEnumerable<Product>> GetProducts(CancellationToken cts)
    {
        var products = await _unitOfWork.ProductRepository.GetAll(cts);
        return products;
    }

    public virtual async Task<Product> GetProduct(Guid id, CancellationToken cts)
    {
        var product = await _unitOfWork.ProductRepository.GetById(id, cts);
        return product;
    }

    public virtual async Task<Product> AddProduct(string name, decimal price, CancellationToken cts)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var product = new Product(Guid.NewGuid(), name, price);
        await _unitOfWork.ProductRepository.Add(product, cts);
        await _unitOfWork.SaveChangesAsync(cts);
        return product;
    }

    public virtual async Task<Product> UpdateProduct(string name, CancellationToken cts)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var product = await _unitOfWork.ProductRepository.GetByName(name, cts);
        await _unitOfWork.ProductRepository.Update(product, cts);
        await _unitOfWork.SaveChangesAsync(cts);
        return product;
    }

    public virtual async Task<Product> DeleteProduct(Guid id, CancellationToken cts)
    {
        var product = await _unitOfWork.ProductRepository.DeleteById(id, cts);
        await _unitOfWork.SaveChangesAsync(cts);
        return product;
    }
}