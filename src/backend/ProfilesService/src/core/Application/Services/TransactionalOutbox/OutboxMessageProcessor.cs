using System.Runtime.CompilerServices;
using Application.Abstractions.DomainEvents;
using Application.Abstractions.Repositories.OutBox;
using Application.Abstractions.TransactionalOutbox;
using Application.OperationResult.Builders;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;

namespace Application.Services.TransactionalOutbox;

public class OutboxMessageProcessor
    (IOutboxMessagesRepository<OutboxMessage> outboxMessagesRepository,
        IDomainEventSender domainEventSender) : IOutboxMessageProcessor
{
    public async IAsyncEnumerable<OperationResult<bool>> ProcessAsync(IEnumerable<IOutboxMessage?> messages, [EnumeratorCancellation] CancellationToken cancellationToken = default)
    {
        foreach (var message in messages)
        {
            if (message is null)
            {
                yield return OperationResultBuilder.Failure();
                continue;
            }
            
            var domainEvent = message.GetDomainEvent();
            
            if (domainEvent is null)
            {
                yield return OperationResultBuilder.Failure();
                continue;
            }
            
            await domainEventSender.SendAsync(domainEvent, cancellationToken);
            message.Processed();
            await outboxMessagesRepository.UpdateAsync((message as OutboxMessage)!, cancellationToken);
            
            yield return OperationResultBuilder.Success(true);
        }
        
        await outboxMessagesRepository.SaveChangesAsync(cancellationToken);
    }
}