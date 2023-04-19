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
    public async Task<ActionResult<CategoriesResponse>> GetCategories(CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetCategories(cancellationToken);
        return new CategoriesResponse(categories.Select(_mapper.MapCategoryModel));
    }
    
    [HttpGet("get_by_parent_id")]
    public async Task<ActionResult<CategoriesResponse>> GetCategoriesByParentId(Guid parentId, CancellationToken cancellationToken)
    {
        var categories = await _categoryService.GetCategoriesByParentId(parentId, cancellationToken);
        return new CategoriesResponse(categories.Select(_mapper.MapCategoryModel));
    }

    [HttpGet("get_by_id")]
    public async Task<ActionResult<CategoryResponse>> GetCategory(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.GetCategory(id, cancellationToken);
        return new CategoryResponse(category.ParentId,category.Id, category.Name);
    }


    [HttpPost("add")]
    public async Task<ActionResult<CategoryResponse>> AddCategory(CategoryRequest request, CancellationToken cancellationToken)
    {
        var category = await _categoryService.AddCategory(request.Name, request.ParentId, cancellationToken);
        return new CategoryResponse(category.ParentId,category.Id, category.Name);
    }

    [HttpPut("update")]
    public async Task<ActionResult<CategoryResponse>> UpdateCategory(Guid id, string newName, CancellationToken cancellationToken)
    {
        var category = await _categoryService.UpdateCategory(id, newName, cancellationToken);
        return new CategoryResponse(category.ParentId,category.Id, category.Name);
    }

    [HttpDelete("delete_by_id")]
    public async Task<ActionResult<CategoryResponse>> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var category = await _categoryService.DeleteCategory(id, cancellationToken);
        return new CategoryResponse(category.ParentId,category.Id, category.Name);
    }
}