using Application.Abstractions.Persistence.Repositories.Specification;
using Application.Dtos.Requests;
using Domain.Models;

namespace Application.Abstractions.Persistence.Repositories;

public interface IReadOfficesRepository
{
    public Task AddAsync(Office entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(Office entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(Office entity, CancellationToken cancellationToken = default);

    public Task<Office?> GetByAsync(IBaseSpecification<Office> specification,
        CancellationToken cancellationToken = default); 
    public Task<IReadOnlyList<Office?>?> GetManyByAsync(IBaseSpecification<Office> specification,PageSettings pageSettings, CancellationToken cancellationToken = default);
}