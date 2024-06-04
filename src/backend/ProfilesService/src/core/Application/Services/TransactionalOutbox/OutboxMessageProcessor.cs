using System.Collections.Frozen;
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
    public async IAsyncEnumerable<OperationResult<OutboxMessage>> ProcessAsync(FrozenSet<OutboxMessage> messages,
        [EnumeratorCancellation] CancellationToken cancellationToken)
    {
        foreach (var message in messages)
        {
            var domainEvent = message.GetDomainEvent();
            
            if (domainEvent is null)
            {
                yield return OperationResultBuilder.Failure<OutboxMessage>(new Error($"Message {message.Id} has no domain event"));
                continue;
            }
            
            await domainEventSender.SendAsync(domainEvent, cancellationToken);
            message.Processed();
            await outboxMessagesRepository.UpdateAsync(message, cancellationToken);
            
            yield return OperationResultBuilder.Success(message);
        }
        
        await outboxMessagesRepository.SaveChangesAsync(cancellationToken);
    }
}