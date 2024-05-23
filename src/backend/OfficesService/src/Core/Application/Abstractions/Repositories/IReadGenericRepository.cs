using Domain.Abstractions;

namespace Application.Abstractions.Repositories;

public interface IReadGenericRepository<T> where T : Entity
{
    public Task AddAsync(T entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(T entity, CancellationToken cancellationToken = default);
}