using System.Linq.Expressions;
using Application.Abstractions.Persistence.Repositories.Specification;
using Application.Dtos.Requests;
using Domain.Abstractions;

namespace Application.Abstractions.Persistence.Repositories;

public interface IReadGenericRepository<T> where T : Entity
{
    public Task AddAsync(T entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
    public Task<TK?> GetByAsync<TK>(IBaseSpecification<T> specification, CancellationToken cancellationToken = default);
    public Task<IReadOnlyList<TK>> GetManyByAsync<TK>(IBaseSpecification<T> specification,PageSettings pageSettings, CancellationToken cancellationToken = default);
}