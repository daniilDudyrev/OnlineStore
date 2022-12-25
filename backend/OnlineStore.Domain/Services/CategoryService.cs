using OnlineStore.Domain.Entities;
using OnlineStore.Domain.RepositoryInterfaces;

namespace OnlineStore.Domain.Services;

public class CategoryService
{
    private readonly IUnitOfWork _unitOfWork;

    public CategoryService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
    }

    public virtual async Task<IReadOnlyCollection<Category>> GetCategories(CancellationToken cts) =>
        await _unitOfWork.CategoryRepository.GetAll(cts);

    public virtual async Task<Category> GetCategory(Guid id, CancellationToken cts) =>
        await _unitOfWork.CategoryRepository.GetById(id, cts);

    public virtual async Task<Category> AddCategory(string name, CancellationToken cts)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }

        var category = new Category(Guid.NewGuid(), name);
        await _unitOfWork.CategoryRepository.Add(category, cts);
        await _unitOfWork.SaveChangesAsync(cts);
        return category;
    }

    public virtual async Task<Category> UpdateCategory(string name, CancellationToken cts)
    {
        if (name == null)
        {
            throw new ArgumentNullException(nameof(name));
        }
        var category = await _unitOfWork.CategoryRepository.GetByName(name, cts);
        await _unitOfWork.CategoryRepository.Update(category, cts);
        await _unitOfWork.SaveChangesAsync(cts);
        return category;
    }

    public virtual async Task<Category> DeleteCategory(Guid id, CancellationToken cts)
    {
        var category = await _unitOfWork.CategoryRepository.DeleteById(id, cts);
        await _unitOfWork.SaveChangesAsync(cts);
        return category;
    }
}