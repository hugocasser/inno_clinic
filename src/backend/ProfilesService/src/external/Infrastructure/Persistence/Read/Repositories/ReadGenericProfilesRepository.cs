using System.Runtime.CompilerServices;
using Application.Abstractions.Repositories.Read;
using Application.Abstractions.TransactionalOutbox;
using Application.Dtos.Requests;
using Application.Specifications;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Read.Repositories;

public abstract class ReadGenericProfilesRepository<TReadModel>(ProfilesReadDbContext context)
    : IReadGenericProfilesRepository<TReadModel>
    where TReadModel : class, IReadProfileModel
{
    public async Task<TReadModel?> GetByIdFromDeletedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        using var cursor = await context.Collection<TReadModel>()
            .FindAsync(x => x.Id == id && x.IsDeleted, cancellationToken: cancellationToken);
        
        return await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(TReadModel entity, CancellationToken cancellationToken = default)
    {
        await context.Collection<TReadModel>().InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(TReadModel entity, CancellationToken cancellationToken = default)
    {
        await context.Collection<TReadModel>().ReplaceOneAsync
            (x => x.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(TReadModel entity, CancellationToken cancellationToken = default)
    {
        await context.Collection<TReadModel>()
            .ReplaceOneAsync(model => model.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }

    public async Task<TReadModel?> GetByAsync(FilterDefinition<TReadModel> filter, CancellationToken cancellationToken = default)
    {
        using var cursor = await context.Collection<TReadModel>()
            .FindAsync(filter, cancellationToken: cancellationToken);
        
        var entity = await cursor.FirstOrDefaultAsync(cancellationToken: cancellationToken);
        
        return entity;
    }

    public async IAsyncEnumerable<TReadModel> GetByManyAsync(FilterDefinition<TReadModel> filter, PageSettings pageSettings, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        using var cursor = await context.Collection<TReadModel>()
            .FindAsync(filter, GeneralFindOptions.FromPageSettings<TReadModel>(pageSettings), cancellationToken: cancellationToken);

        while (await cursor.MoveNextAsync(cancellationToken))
        {
            foreach (var model in cursor.Current)
            {
                yield return model;
            }
        }
    }
}