using Application.Abstractions.Persistence.Repositories;
using Application.Outbox;
using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repositories;

public class WriteOfficesRepository(OfficesWriteDbContext context) : IWriteOfficesRepository
{
    public async Task AddOfficeAsync(Office office, CancellationToken cancellationToken = default)
    {
        await context.Offices.AddAsync(office, cancellationToken);
    }

    public Task UpdateOfficeAsync(Office office, CancellationToken cancellationToken = default)
    {
        context.Offices.Update(office);
        return Task.CompletedTask;
    }

    public async Task DeleteOfficeAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var office = await context.Offices.FirstOrDefaultAsync(office => office.Id == officeId, cancellationToken);
        
        if (office != null)
        {
            context.Offices.Remove(office);
        }
    }

    public async Task<Office?> GetOfficeAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        var office = await context.Offices.FirstOrDefaultAsync(office => office.Id == officeId, cancellationToken);
        return office;
    }

    public async Task<IReadOnlyList<OutboxMessage>> GetNotProcessedMessagesAsync(int count, CancellationToken cancellationToken = default)
    {
        return await context
            .OutboxMessages
            .Include(outbox => outbox.Entity)
            .Where(outboxMessage => outboxMessage.ProcessedAt == null)
            .OrderBy(outboxMessage => outboxMessage.CreatedAt)
            .Take(count)
            .ToListAsync(cancellationToken);
    }
}