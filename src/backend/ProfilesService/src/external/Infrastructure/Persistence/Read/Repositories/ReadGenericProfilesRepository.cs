using Application.Abstractions.Repositories.Read;
using Application.Abstractions.TransactionalOutbox;
using Application.Dtos.Requests;
using Application.Specifications;
using Domain.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Read.Repositories;

public abstract class ReadGenericProfilesRepository<TReadModel, TModel>(ProfilesReadDbContext context) : IReadGenericProfilesRepository<TReadModel, TModel> 
    where TReadModel : class, IReadProfileModel<TModel> where TModel : Profile 
{
    public async Task<TReadModel?> GetByIdFromDeletedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var model = await context.Collection<TReadModel, TModel>()
            .FindAsync(x => x.Id == id && x.IsDeleted, cancellationToken: cancellationToken);
        
        return await model.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(TReadModel entity, CancellationToken cancellationToken = default)
    {
        await context.Collection<TReadModel, TModel>().InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(TReadModel entity, CancellationToken cancellationToken = default)
    {
        await context.Collection<TReadModel, TModel>().ReplaceOneAsync
            (x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(TReadModel entity, CancellationToken cancellationToken = default)
    {
        await context.Collection<TReadModel, TModel>()
            .ReplaceOneAsync(model => model.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }

    public async Task<TReadModel?> GetByAsync(FilterDefinition<TReadModel> filter, CancellationToken cancellationToken = default)
    {
        var cursor = await context.Collection<TReadModel, TModel>()
            .FindAsync(filter, cancellationToken: cancellationToken);
        var query = await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        return query;
    }

    public async Task<List<TReadModel>> GetByManyAsync(FilterDefinition<TReadModel> filter, PageSettings pageSettings, CancellationToken cancellationToken = default)
    {
        var cursor = await context.Collection<TReadModel, TModel>()
            .FindAsync(filter, GeneralFindOptions.FromPageSettings<TReadModel>(pageSettings), cancellationToken: cancellationToken);
        var query = await cursor.ToListAsync(cancellationToken);
        
        return query;
    }
}