using Application.Abstractions.TransactionalOutbox;
using Domain.Abstractions;

namespace Application.Abstractions.Repositories.Read;

public interface IReadGenericRepository<TReadModel, TModel> where TReadModel : IReadProfileModel<TModel> where TModel : Profile
{
    public Task<TReadModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    public Task AddAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task UpdateAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task DeleteAsync(TReadModel entity, CancellationToken cancellationToken = default);
    public Task<IReadProfileModel<TReadModel>> GetByAsync(CancellationToken cancellationToken = default);
    public Task<IEnumerable<IReadProfileModel<TReadModel>>> GetByManyAsync(CancellationToken cancellationToken = default);
}