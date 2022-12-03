using Microsoft.AspNetCore.Mvc;
using OnlineStore.Data.Repositories;
using OnlineStore.Domain;

namespace OnlineStore.WebApi.Controllers;

[Route("products")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public ProductController(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    [HttpGet("get_all")]
    public async Task<IEnumerable<Product>> GetProducts(CancellationToken cts)
    {
        var product = await _productRepository.GetAll(cts);
        return product;
    }

    [HttpGet("get_by_id")]
    public async Task<Product> GetProduct([FromQuery] Guid id, CancellationToken cts)
    {
        var product = await _productRepository.GetById(id, cts);
        return product;
    }

    [HttpPost("add")]
    public async Task AddProduct(Product product, CancellationToken cts)
    {
        await _productRepository.Add(product, cts);
    }

    [HttpPut("update")]
    public async Task UpdateProduct(Product product, CancellationToken cts)
    {
        await _productRepository.Update(product, cts);
    }

    [HttpDelete("delete_by_id")]
    public async Task DeleteProduct(Guid id, CancellationToken cts)
    {
        await _productRepository.DeleteById(id, cts);
    }
}