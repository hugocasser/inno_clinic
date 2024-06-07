using Application.Abstractions.Repositories.Outbox;
using Application.Services.TransactionalOutbox;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Write.Repositories;

public class OutboxMessagesRepository(ProfilesWriteDbContext context) : IOutboxMessagesRepository
{
    public Task UpdateAsync(OutboxMessage message)
    {
        context.OutboxMessages.Update(message);
        
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync(CancellationToken cancellationToken)
    {
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<OutboxMessage>> GetNotProcessedAsync(int count, CancellationToken cancellationToken)
    {
        return await 
            context.OutboxMessages
            .Where(x => x.ProcessedAt == null)
            .OrderByDescending(x => x.CreatedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}