using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Entities;
using OnlineStore.Domain.Services;
using OnlineStore.Models.Requests;
using OnlineStore.Models.Responses;
using OnlineStore.WebApi.Mappers;

namespace OnlineStore.WebApi.Controllers;

[Route("categories")]
public class CategoryController : ControllerBase
{
    private readonly CategoryService _categoryService;
    private readonly HttpModelsMapper _mapper;


    public CategoryController(CategoryService categoryService, HttpModelsMapper mapper)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("get_all")]
    public async Task<ActionResult<CategoriesResponse>> GetCategories(CancellationToken cts)
    {
        var categories = await _categoryService.GetCategories(cts);
        return new CategoriesResponse(categories.Select(_mapper.MapCategoryModel));
    }

    [HttpGet("get_by_id")]
    public async Task<ActionResult<CategoryResponse>> GetCategory(Guid id, CancellationToken cts)
    {
        var category = await _categoryService.GetCategory(id, cts);
        return new CategoryResponse(category.Id, category.Name);
    }


    [HttpPost("add")]
    public async Task<ActionResult<CategoryResponse>> AddCategory(CategoryRequest request, CancellationToken cts)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var category = await _categoryService.AddCategory(request.Name, cts);
        return new CategoryResponse(category.Id, category.Name);
    }

    [HttpPut("update")]
    public async Task<ActionResult<CategoryResponse>> UpdateCategory(CategoryRequest request, CancellationToken cts)
    {
        if (request == null)
        {
            throw new ArgumentNullException(nameof(request));
        }

        var category = await _categoryService.UpdateCategory(request.Name, cts);
        return new CategoryResponse(category.Id, category.Name);
    }

    [HttpDelete("delete_by_id")]
    public async Task<ActionResult<CategoryResponse>> DeleteCategory(Guid id, CancellationToken cts)
    {
        var category = await _categoryService.DeleteCategory(id, cts);
        return new CategoryResponse(category.Id, category.Name);
    }
}