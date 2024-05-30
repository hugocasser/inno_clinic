using Application.OperationResult.Results;

namespace Application.Abstractions.TransactionalOutbox;

public interface IOutboxMessageProcessor
{
    public Task<OperationResult<bool>> ProcessAsync(IOutboxMessage? message,CancellationToken cancellationToken);
}