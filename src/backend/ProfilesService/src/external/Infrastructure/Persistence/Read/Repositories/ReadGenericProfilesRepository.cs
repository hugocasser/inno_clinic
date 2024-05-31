using Application.Abstractions.Repositories.Read;
using Application.Abstractions.TransactionalOutbox;
using Domain.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Read.Repositories;

public abstract class ReadGenericProfilesRepository<TReadModel, TModel>(ProfilesReadDbContext context) : IReadGenericProfilesRepository<TReadModel, TModel> 
    where TReadModel : class, IReadProfileModel<TModel> where TModel : Profile 
{
    public async Task<TReadModel> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var model = await context.Collection<TReadModel, TModel>()
            .FindAsync(x => x.Id == id, cancellationToken: cancellationToken);
        
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
        await context.Collection<TReadModel, TModel>().DeleteOneAsync
            (x => x.Id == entity.Id, cancellationToken: cancellationToken);
    }

    public async Task<TReadModel> GetByAsync(FilterDefinition<TReadModel> filter, CancellationToken cancellationToken = default)
    {
        var query = await context.Collection<TReadModel, TModel>().FindAsync(filter, cancellationToken: cancellationToken);
        
        return await query.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task<List<TReadModel>> GetByManyAsync(FilterDefinition<TReadModel> filter, CancellationToken cancellationToken = default)
    {
        var query = await context.Collection<TReadModel, TModel>().FindAsync(filter, cancellationToken: cancellationToken);
        
        return await query.ToListAsync(cancellationToken);
    }
}