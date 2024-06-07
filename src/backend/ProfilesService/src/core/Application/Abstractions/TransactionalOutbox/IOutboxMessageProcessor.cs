using System.Collections.Frozen;
using Application.OperationResult.Results;
using Application.Services.TransactionalOutbox;

namespace Application.Abstractions.TransactionalOutbox;

public interface IOutboxMessageProcessor
{
    public IAsyncEnumerable<OperationResult<OutboxMessage>> ProcessAsync(FrozenSet<OutboxMessage> message,
        CancellationToken cancellationToken);
}