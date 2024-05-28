using Application.Outbox;
using Domain.Abstractions;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace Infrastructure.Interceptors;

public class ConvertDomainEventsToOutboxMessagesInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var context = eventData.Context;

        if (context is null)
        {
            return base.SavingChangesAsync(eventData, result, cancellationToken);
        }
        
        var messages = context
            .ChangeTracker
            .Entries<Entity>()
            .Select(e => new OutboxMessage
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                ProcessedAt = null,
                SerializedEvent = JsonConvert.SerializeObject(e.Entity, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                    TypeNameHandling = TypeNameHandling.All
                }),
            }).ToList();
        
        context.Set<OutboxMessage>().AddRange(messages);
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}