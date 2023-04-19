using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;
using OnlineStore.Models.Requests;
using OnlineStore.Models.Responses;
using OnlineStore.WebApi.Mappers;

namespace OnlineStore.WebApi.Controllers;

[Route("products")]
public class ProductController : ControllerBase
{
    private readonly ProductService _productService;
    private readonly HttpModelsMapper _mapper;

    public ProductController(ProductService productService, HttpModelsMapper mapper)
    {
        _productService = productService ?? throw new ArgumentNullException(nameof(productService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }
    
    [HttpGet("get_all")]
    public async Task<ActionResult<ProductsResponse>> GetProducts(CancellationToken cancellationToken)
    {
        var products = await _productService.GetProducts(cancellationToken);
        return new ProductsResponse(products.Select(_mapper.MapProductModel));
    }

    [HttpGet("get_by_category")]
    public async Task<ActionResult<ProductsResponse>> GetProductsByCategoryId(Guid categoryId, CancellationToken cancellationToken)
    {
        var products = await _productService.GetProductsByCategoryId(categoryId, cancellationToken);
        return new ProductsResponse(products.Select(_mapper.MapProductModel));
    }

    [HttpGet("get_by_id")]
    public async Task<ActionResult<ProductResponse>> GetProduct(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.GetProduct(id, cancellationToken);
        return new ProductResponse(product.Id, product.Name, product.Price, product.Image, product.Description,
            product.CategoryId);
    }

    
    [HttpPost("add")]
    public async Task<ActionResult<ProductResponse>> AddProduct(ProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productService.AddProduct(request.Name, request.Price, request.Image, request.Description,
            request.CategoryId, cancellationToken);
        return new ProductResponse(product.Id, product.Name, product.Price, product.Image, product.Description,
            product.CategoryId);
    }
    
    [HttpPut("update")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(ProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _productService.UpdateProduct(request.Name, request.Price, request.Image,
            request.Description, request.CategoryId, cancellationToken);
        return new ProductResponse(product.Id, product.Name, product.Price, product.Image, product.Description,
            product.CategoryId);
    }

    
    [HttpDelete("delete_by_id")]
    public async Task<ActionResult<ProductResponse>> DeleteProduct(Guid id, CancellationToken cancellationToken)
    {
        var product = await _productService.DeleteProduct(id, cancellationToken);
        return new ProductResponse(product.Id, product.Name, product.Price, product.Image, product.Description,
            product.CategoryId);
    }
}