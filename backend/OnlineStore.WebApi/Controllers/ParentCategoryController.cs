using Microsoft.AspNetCore.Mvc;
using OnlineStore.Domain.Services;
using OnlineStore.Models.Requests;
using OnlineStore.Models.Responses;
using OnlineStore.WebApi.Mappers;

namespace OnlineStore.WebApi.Controllers;

[Route("parentCategories")]
public class ParentCategoryController : ControllerBase
{
    private readonly ParentCategoryService _parentCategoryService;
    private readonly HttpModelsMapper _mapper;


    public ParentCategoryController(ParentCategoryService parentCategoryService, HttpModelsMapper mapper)
    {
        _parentCategoryService = parentCategoryService ?? throw new ArgumentNullException(nameof(parentCategoryService));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [HttpGet("get_all")]
    public async Task<ActionResult<ParentCategoriesResponse>> GetCategories(CancellationToken cancellationToken)
    {
        var parentCategories = await _parentCategoryService.GetCategories(cancellationToken);
        return new ParentCategoriesResponse(parentCategories.Select(_mapper.MapParentCategoryModel));
    }

    [HttpGet("get_by_id")]
    public async Task<ActionResult<ParentCategoryResponse>> GetCategory(Guid id, CancellationToken cancellationToken)
    {
        var parentCategory = await _parentCategoryService.GetCategory(id, cancellationToken);
        return new ParentCategoryResponse(parentCategory.Id, parentCategory.Name);
    }


    [HttpPost("add")]
    public async Task<ActionResult<ParentCategoryResponse>> AddCategory(ParentCategoryRequest request, CancellationToken cancellationToken)
    {
        var parentCategory = await _parentCategoryService.AddCategory(request.Name, cancellationToken);
        return new ParentCategoryResponse(parentCategory.Id, parentCategory.Name);
    }

    [HttpPut("update")]
    public async Task<ActionResult<ParentCategoryResponse>> UpdateCategory(Guid id, string newName, CancellationToken cancellationToken)
    {
        var parentCategory = await _parentCategoryService.UpdateCategory(id, newName, cancellationToken);
        return new ParentCategoryResponse(parentCategory.Id, parentCategory.Name);
    }

    [HttpDelete("delete_by_id")]
    public async Task<ActionResult<ParentCategoryResponse>> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var parentCategory = await _parentCategoryService.DeleteCategory(id, cancellationToken);
        return new ParentCategoryResponse(parentCategory.Id, parentCategory.Name);
    }
}