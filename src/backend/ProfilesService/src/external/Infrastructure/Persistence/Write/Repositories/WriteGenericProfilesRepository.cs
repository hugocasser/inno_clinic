using Application.Abstractions.Repositories.Write;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write.Repositories;

public abstract class WriteGenericProfilesRepository<T>(DbContext context)
    : IWriteGenericProfilesRepository<T> where T : Profile
{
    public async Task<T?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Set<T>().FirstOrDefaultAsync
            (entity => entity.Id == id && !entity.IsDeleted, cancellationToken: cancellationToken);
    }

    public async Task<T?> GetByIdFromDeletedAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await context.Set<T>().FirstOrDefaultAsync
            (entity => entity.Id == id && entity.IsDeleted, cancellationToken: cancellationToken);
    }

    public async Task AddAsync(T entity, CancellationToken cancellationToken = default)
    {
        await context.Set<T>().AddAsync(entity, cancellationToken);
    }

    public Task UpdateAsync(T entity)
    {
        context.Set<T>().Update(entity);
        
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await context.Set<T>().FirstAsync
            (entity => entity.Id == id && !entity.IsDeleted, cancellationToken: cancellationToken);
        
        entity.IsDeleted = true;
        
        context.Set<T>().Update(entity);
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await context.SaveChangesAsync(cancellationToken);
    }
}