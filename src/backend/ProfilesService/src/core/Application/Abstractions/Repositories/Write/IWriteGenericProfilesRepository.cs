using Domain.Abstractions;

namespace Application.Abstractions.Repositories.Write;

public interface IWriteGenericProfilesRepository<T> where T : Profile
{
    public Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    public Task AddAsync(T entity, CancellationToken cancellationToken = default);
    
    public Task UpdateAsync(T entity, CancellationToken cancellationToken = default);
    
    public Task DeleteAsync(Guid id, CancellationToken cancellationToken = default);
    
    public Task SaveChangesAsync(CancellationToken cancellationToken = default);
}