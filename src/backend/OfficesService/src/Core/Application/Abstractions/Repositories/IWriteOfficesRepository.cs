using Domain.Models;

namespace Application.Abstractions.Repositories;

public interface IWriteOfficesRepository
{
    public Task AddOfficeAsync(Office office);
    public Task UpdateOfficeAsync(Office office);
    public Task DeleteOfficeAsync(Guid officeId);
    public Task<Office?> GetOfficeAsync(Guid officeId);
}