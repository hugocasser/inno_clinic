using Application.Abstractions.DomainEvents;
using Application.Abstractions.Repositories.Read;
using Application.ReadModels;
using Domain.Abstractions.DomainEvents;
using Domain.DomainEvents;

namespace Application.DomainEventsHandler;

public class ReceptionistDomainEventHandler(IReadReceptionistsRepository readReceptionistsRepository) : IDomainEventHandler<ReceptionistDomainEvent>
{
    public async Task HandleAsync(ReceptionistDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var readModel = ReceptionistReadModel.MapToReadModel(domainEvent.GetEntity());
        
        switch (domainEvent.GetEventType())
        {
            case EventType.Created:
            {
                await readReceptionistsRepository.AddAsync(readModel, cancellationToken);
                break;
            }
            case EventType.Updated:
            {
                await readReceptionistsRepository.UpdateAsync(readModel, cancellationToken);
                break;
            }
            case EventType.Deleted:
            {
                await readReceptionistsRepository.DeleteAsync(readModel, cancellationToken);
                break;
            }
            default:
            {
                var exception = new ArgumentOutOfRangeException
                {
                    HelpLink = typeof(EventType).Assembly.FullName + typeof(EventType).FullName,
                    HResult = -1,
                    Source = typeof(ReceptionistDomainEventHandler).Assembly.FullName + typeof(ReceptionistDomainEventHandler).FullName
                };

                throw exception;
            }
        }
    }
}