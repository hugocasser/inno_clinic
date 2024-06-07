using Application.Abstractions.DomainEvents;
using Domain.Abstractions.DomainEvents;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.DomainEvents;

public class DomainEventSender(IServiceProvider serviceProvider) : IDomainEventSender
{
    public async Task SendAsync<T>(T? domainEvent, CancellationToken cancellationToken) where T : IDomainEvent
    {
        if (domainEvent is null)
        {
            return;
        }
        var handler = serviceProvider.GetService<IDomainEventHandler<T>>();
        
        if (handler is null)
        {
            return;
        }
        
        await handler.HandleAsync(domainEvent, cancellationToken);
    }
}