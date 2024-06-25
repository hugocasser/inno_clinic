using DLL.Models;

namespace DLL.Abstractions.Persistence.Repositories;

public interface ICategoriesRepository : IBaseRepository
{
    public Task<bool> AddAsync(Category category, CancellationToken cancellationToken);
    public Task<IReadOnlyCollection<Category>> GetAllAsync();
    public Task<bool> IsExistsAsync(Guid requestCategoryId);
}