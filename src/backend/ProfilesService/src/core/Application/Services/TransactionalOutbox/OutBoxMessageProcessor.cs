using Application.Abstractions.TransactionalOutbox;
using Application.OperationResult.Builders;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;

namespace Application.Services.TransactionalOutbox;

public class OutBoxMessageProcessor(IServiceProvider serviceProvider) : IOutboxMessageProcessor
{
    public async Task<OperationResult<bool>> ProcessAsync(IOutboxMessage? message, CancellationToken cancellationToken)
    {
        if (message is null)
        {
            return OperationResultBuilder.Failure(new Error("message is null"));
        }
        
        return OperationResultBuilder.Success();
    }
}