using Application.Services.TransactionalOutbox;

namespace Application.Abstractions.Repositories.Outbox;

public interface IOutboxMessagesRepository
{ 
    public Task UpdateAsync(OutboxMessage message);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    public Task<List<OutboxMessage>> GetNotProcessedAsync(int count, CancellationToken cancellationToken);
}