using Domain.Abstractions.DomainEvents;

namespace Application.Abstractions.DomainEvents;

public interface IDomainEventSender
{
    public Task SendAsync<T>(T? domainEvent, CancellationToken cancellationToken) where T : IDomainEvent;
}