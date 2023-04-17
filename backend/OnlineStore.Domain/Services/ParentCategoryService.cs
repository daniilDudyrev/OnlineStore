using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Domain.Services;

public class ParentCategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public ParentCategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public virtual async Task<IReadOnlyCollection<ParentCategory>> GetCategories(CancellationToken cancellationToken) =>
        await _unitOfWork.ParentCategoryRepository.GetAll(cancellationToken);

    public virtual async Task<ParentCategory> GetCategory(Guid id, CancellationToken cancellationToken) =>
        await _unitOfWork.ParentCategoryRepository.GetById(id, cancellationToken);

    public virtual async Task<ParentCategory> AddCategory(string name, CancellationToken cancellationToken)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var parentCategory = new ParentCategory(Guid.NewGuid(), name);
        await _unitOfWork.ParentCategoryRepository.Add(parentCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return parentCategory;
    }

    public virtual async Task<ParentCategory> UpdateCategory(Guid parentCategoryId, string newName, CancellationToken cancellationToken)
    {
        if (newName == null)
        {
            throw new ArgumentNullException(nameof(newName));
        }

        var parentCategory = await _unitOfWork.ParentCategoryRepository.GetById(parentCategoryId, cancellationToken);
        parentCategory.Name = newName;
        await _unitOfWork.ParentCategoryRepository.Update(parentCategory, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return parentCategory;
    }

    public virtual async Task<ParentCategory> DeleteCategory(Guid id, CancellationToken cancellationToken)
    {
        var parentCategory = await _unitOfWork.ParentCategoryRepository.DeleteById(id, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return parentCategory;
    }
}