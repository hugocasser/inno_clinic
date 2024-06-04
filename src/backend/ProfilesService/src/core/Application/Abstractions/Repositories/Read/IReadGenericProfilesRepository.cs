using Application.Abstractions.TransactionalOutbox;
using Application.Dtos.Requests;
using Ardalis.Specification;
using Domain.Abstractions;
using MongoDB.Driver;

namespace Application.Abstractions.Repositories.Read;

public interface IReadGenericProfilesRepository<TReadModel, TModel> where TReadModel : IReadProfileModel<TModel> where TModel : Profile
{
    public Task<TReadModel?> GetByIdFromDeletedAsync(Guid id, CancellationToken cancellationToken = default);
    public Task AddAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task<TReadModel?> GetByAsync( FilterDefinition<TReadModel> filter,
        CancellationToken cancellationToken = default);
    public Task<List<TReadModel>> GetByManyAsync(FilterDefinition<TReadModel> filter, PageSettings pageSettings,
        CancellationToken cancellationToken = default);
}