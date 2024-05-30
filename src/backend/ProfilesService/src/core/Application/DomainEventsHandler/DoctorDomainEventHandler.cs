using Application.Abstractions.DomainEvents;
using Application.Abstractions.Repositories.Read;
using Application.ReadModels;
using Domain.Abstractions.DomainEvents;
using Domain.DomainEvents;
using Domain.Models;

namespace Application.DomainEventsHandler;

public class DoctorDomainEventHandler(IReadDoctorsRepository readRepository) : IDomainEventHandler<DoctorDomainEvent>
{
    public async Task HandleAsync(DoctorDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var readModel = DoctorReadModel.MapToReadModel(domainEvent.GetEntity() as Doctor);
        switch (domainEvent.GetEventType())
        {
            case EventType.Created:
            {
                await readRepository.AddAsync(readModel, cancellationToken);
                break;
            }
            case EventType.Updated:
            {
                await readRepository.UpdateAsync(readModel, cancellationToken);
                break;
            }
            case EventType.Deleted:
            {
                await readRepository.DeleteAsync(readModel, cancellationToken);
                break;
            }
            default:
            {
                var exception = new ArgumentOutOfRangeException
                {
                    HelpLink = typeof(EventType).Assembly.FullName + typeof(EventType).FullName,
                    HResult = -1,
                    Source = typeof(DoctorDomainEventHandler).Assembly.FullName + typeof(DoctorDomainEventHandler).FullName
                };

                throw exception;
            }
        }
    }
}