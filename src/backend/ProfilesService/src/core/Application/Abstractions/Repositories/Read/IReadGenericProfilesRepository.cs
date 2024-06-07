using Application.Abstractions.TransactionalOutbox;
using Application.Dtos.Requests;
using MongoDB.Driver;

namespace Application.Abstractions.Repositories.Read;

public interface IReadGenericProfilesRepository<TReadModel> where TReadModel : IReadProfileModel
{
    public Task<TReadModel?> GetByIdFromDeletedAsync(Guid id, CancellationToken cancellationToken = default);
    public Task AddAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task<TReadModel?> GetByAsync( FilterDefinition<TReadModel> filter,
        CancellationToken cancellationToken = default);
    public IAsyncEnumerable<TReadModel> GetByManyAsync(FilterDefinition<TReadModel> filter, PageSettings pageSettings,
        CancellationToken cancellationToken = default);
}