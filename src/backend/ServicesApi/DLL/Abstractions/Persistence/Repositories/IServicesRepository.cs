using System.Data;
using DLL.Dtos;
using DLL.Models;

namespace DLL.Abstractions.Persistence.Repositories;

public interface IServicesRepository : IBaseRepository
{
    public Task<int> AddAsync(Service service);
    public Task<IEnumerable<Service>> GetAllAsync(int take = 10, int skip = 0);
    public Task<int> UpdateStatusAsync(Guid id, bool status);
    public Task<int> UpdateAsync(Service service);
    public Task<bool> IsExistsAsync(Guid id);
    public Task<Service?> GetByIdAsync(Guid id);
    public Task<int> DeleteAsync(Guid serviceId);
    public Task<ServiceFullDto?> GetFullByIdAsync(Guid id);
};