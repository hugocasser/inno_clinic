using DLL.Models;

namespace DLL.Abstractions.Persistence.Repositories;

public interface ISpecializationsRepository : IBaseRepository
{
    public Task<int> AddAsync(Specialization specialization);
    public Task<IEnumerable<Specialization>> GetAllAsync(int take = 10, int skip = 0);
    public Task<int> UpdateStatusAsync(Guid id, bool status);
    public Task<int> UpdateAsync(Specialization specialization, bool status);
    public Task<bool> IsExistsAsync(Guid id);
    public Task<Specialization?> GetByIdAsync(Guid id);
};