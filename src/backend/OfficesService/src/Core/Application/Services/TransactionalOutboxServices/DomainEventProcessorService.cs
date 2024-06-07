using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Services.TransactionalOutboxServices;
using Domain.Abstractions.Events;
using Domain.Models;

namespace Application.Services.TransactionalOutboxServices;

public class DomainEventProcessorService(
    IReadOfficesRepository readRepository): IDomainEventProcessorService
{
    public async Task ProcessDomainEventMessagesAsync
        (IDomainEvent<Office> domainEvent, CancellationToken cancellationToken = default)
    {
        switch (domainEvent.GetEventType())
        {
            case nameof(EventType.Created):
            {
                await readRepository.AddAsync(domainEvent.GetEntity(), cancellationToken);
                break;
            }
            case nameof(EventType.Updated):
            {
                await readRepository.UpdateAsync(domainEvent.GetEntity(), cancellationToken);
                break;
            }
            case nameof(EventType.Deleted):
            {
                await readRepository.DeleteAsync(domainEvent.GetEntity(), cancellationToken);
                break;
            }
        }
    }
}