using Application.Abstractions.TransactionalOutbox;
using Application.Services.TransactionalOutbox;

namespace Application.Abstractions.Repositories.OutBox;

public interface IOutboxMessagesRepository<in T> where T : IOutboxMessage
{ 
    public Task UpdateAsync(T message, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
    
    public Task<List<OutboxMessage>> GetNotProcessedAsync(int count, CancellationToken cancellationToken);
}