using Application.Services.TransactionalOutbox;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Interceptors;

public class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        if (eventData.Context is null)
        {
            return await base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        var context = eventData.Context;

        var domainEvents = context.ChangeTracker
            .Entries<Profile>()
            .Where(entry => entry.State is not (EntityState.Unchanged or EntityState.Detached))
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            })
            .Select(OutboxMessage.Create);

        await context.Set<OutboxMessage>().AddRangeAsync(domainEvents, cancellationToken);
        
        return await base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}