using Application.Abstractions.DomainEvents;
using Application.Abstractions.Repositories.Read;
using Application.ReadModels;
using Domain.Abstractions.DomainEvents;
using Domain.DomainEvents;
using Domain.Models;

namespace Application.DomainEventsHandler;

public class PatientDomainEventHandler(IReadPatientRepository readRepository) : IDomainEventHandler<PatientDomainEvent>
{
    public async Task HandleAsync(PatientDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var readModel = PatientReadModel.MapToReadModel(domainEvent.GetEntity() as Patient);

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
                    Source = typeof(PatientDomainEventHandler).Assembly.FullName +
                        typeof(PatientDomainEventHandler).FullName
                };

                throw exception;
            }
        }
    }
}