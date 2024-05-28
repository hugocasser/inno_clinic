using Application.Abstractions.OperationResult;
using Domain.Abstractions;
using Domain.Abstractions.Events;
using Domain.Models;

namespace Application.Abstractions.Services.TransactionalOutboxServices;

public interface IDomainEventProcessorService
{
    public Task ProcessDomainEventMessagesAsync
        (IDomainEvent<Office> domainEvent, CancellationToken cancellationToken = default);
}