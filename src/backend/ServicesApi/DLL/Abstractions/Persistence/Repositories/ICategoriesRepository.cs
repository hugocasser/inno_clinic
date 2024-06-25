using DLL.Models;

namespace DLL.Abstractions.Persistence.Repositories;

public interface ICategoriesRepository : IBaseRepository
{
    public Task<bool> AddAsync(Category category, CancellationToken cancellationToken);
    public Task<IReadOnlyCollection<Category>> GetAllAsync(int take = 10, int skip = 0, CancellationToken cancellationToken = default); 
}