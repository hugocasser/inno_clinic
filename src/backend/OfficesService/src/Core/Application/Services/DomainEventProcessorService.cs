using Application.Abstractions.OperationResult;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services.TransactionalOutboxServices;
using Application.OperationResults;
using Domain.Abstractions;
using Domain.Abstractions.Events;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public class DomainEventProcessorService(
    IServiceProvider serviceProvider): IDomainEventProcessorService
{
    public async Task ProcessDomainEventMessagesAsync<T>
        (IDomainEvent<T> domainEvent, CancellationToken cancellationToken = default)
        where T : Entity
    {
        using var scope = serviceProvider.CreateScope();
        var readRepository = scope.ServiceProvider
            .GetRequiredService<IReadGenericRepository<T>>();

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