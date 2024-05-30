using Domain.Abstractions.DomainEvents;

namespace Application.Abstractions.DomainEvents;

public interface IDomainEventHandler<in T> where T : IDomainEvent
{
    public Task HandleAsync(T domainEvent, CancellationToken cancellationToken);
}