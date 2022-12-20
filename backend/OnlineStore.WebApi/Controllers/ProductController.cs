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
    public async Task<ActionResult<ProductsResponse>> GetProducts(CancellationToken cts)
    {
        var products = await _productService.GetProducts(cts);
        return new ProductsResponse(products.Select(_mapper.MapProductModel));
    }

    [HttpGet("get_by_id")]
    public async Task<ActionResult<ProductResponse>> GetProduct(Guid id, CancellationToken cts)
    {
        var product = await _productService.GetProduct(id, cts);
        return new ProductResponse(product.Id, product.Name, product.Price);
    }

    [HttpPost("add")]
    public async Task<ActionResult<ProductResponse>> AddProduct(ProductRequest request, CancellationToken cts)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var product = await _productService.AddProduct(request.Name, request.Price, cts);
        return new ProductResponse(product.Id, product.Name, product.Price);
    }

    [HttpPut("update")]
    public async Task<ActionResult<ProductResponse>> UpdateProduct(ProductRequest request, CancellationToken cts)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var product = await _productService.UpdateProduct(request.Name, cts);
        return new ProductResponse(product.Id, product.Name, product.Price);
    }

    [HttpDelete("delete_by_id")]
    public async Task<ActionResult<ProductResponse>> DeleteProduct(Guid id, CancellationToken cts)
    {
        var product = await _productService.DeleteProduct(id, cts);
        return new ProductResponse(product.Id, product.Name, product.Price);
    }
}