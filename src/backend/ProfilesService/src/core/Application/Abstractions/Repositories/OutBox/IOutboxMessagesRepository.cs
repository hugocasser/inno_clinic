using Application.Abstractions.TransactionalOutbox;

namespace Application.Abstractions.Repositories.OutBox;

public interface IOutboxMessagesRepository<in T> where T : IOutboxMessage
{ 
    public Task UpdateAsync(T message, CancellationToken cancellationToken);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}