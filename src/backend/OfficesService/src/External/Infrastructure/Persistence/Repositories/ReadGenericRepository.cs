using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Persistence.Repositories.Specification;
using Application.Dtos.Requests;
using Domain.Models;
using Infrastructure.Abstractions;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Repositories;

public class ReadOfficesRepository(IOfficesReadDbContext dbContext): IReadOfficesRepository
{
    public async Task AddAsync(Office entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Offices.InsertOneAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task UpdateAsync(Office entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Offices.ReplaceOneAsync(office => office.Id == entity.Id, entity, cancellationToken: cancellationToken);
    }

    public async Task DeleteAsync(Office entity, CancellationToken cancellationToken = default)
    {
        await dbContext.Offices.DeleteOneAsync(office => office.Id == entity.Id, cancellationToken: cancellationToken);
    }

    public async Task<Office?> GetByAsync(IBaseSpecification<Office> specification, CancellationToken cancellationToken = default)
    {
        var office = await dbContext.Offices
            .FindAsync(specification.ToExpression(), cancellationToken: cancellationToken);

        return await office.FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Office?>?> GetManyByAsync(IBaseSpecification<Office> specification, PageSettings pageSettings,
        CancellationToken cancellationToken = default)
    {
        var cursor =
            await dbContext.Offices.FindAsync(specification.ToExpression(), cancellationToken: cancellationToken);

        var offices = await cursor
            .ToListAsync(cancellationToken: cancellationToken);

        return offices
            .Skip((pageSettings.Page -1) * pageSettings.ItemsPerPage)
            .Take(pageSettings.ItemsPerPage) as IReadOnlyList<Office?>;
    }
}