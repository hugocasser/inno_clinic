using Application.OperationResult.Results;

namespace Application.Abstractions.TransactionalOutbox;

public interface IOutboxMessageProcessor
{
    public IAsyncEnumerable<OperationResult<bool>> ProcessAsync(IEnumerable<IOutboxMessage> message,
        CancellationToken cancellationToken);
}