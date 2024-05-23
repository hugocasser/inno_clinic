using Application.Abstractions.OperationResult;
using Domain.Abstractions;
using Domain.Abstractions.Events;

namespace Application.Abstractions.Services.TransactionalOutboxServices;

public interface IDomainEventProcessorService
{
    public Task ProcessDomainEventMessagesAsync<T>
        (IDomainEvent<T> domainEvent, CancellationToken cancellationToken = default)
            where T : Entity;
}