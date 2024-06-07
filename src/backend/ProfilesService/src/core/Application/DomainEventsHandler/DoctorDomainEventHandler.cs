using Application.Abstractions.DomainEvents;
using Application.Abstractions.Repositories.Read;
using Application.Abstractions.Services.ExternalServices;
using Application.ReadModels;
using Domain.Abstractions.DomainEvents;
using Domain.DomainEvents;

namespace Application.DomainEventsHandler;

public class DoctorDomainEventHandler
    (IReadDoctorsRepository readRepository,
        ISpecializationsService specializationsService)
    : IDomainEventHandler<DoctorDomainEvent>
{
    public async Task HandleAsync(DoctorDomainEvent domainEvent, CancellationToken cancellationToken)
    {
        var model = domainEvent.GetEntity();
        var readModel = DoctorReadModel.MapToReadModel(model);
        
        switch (domainEvent.GetEventType())
        {
            case EventType.Created:
            {
                var specialization = await specializationsService.GetSpecializationNameAsync(model.SpecializationId, cancellationToken);
                readModel.Specialization = specialization;
                
                await readRepository.AddAsync(readModel, cancellationToken);
                break;
            }
            case EventType.Updated:
            {
                if (domainEvent.SpecializationChanged)
                {
                    var specialization = await specializationsService.GetSpecializationNameAsync(model.SpecializationId, cancellationToken);
                    readModel.Specialization = specialization;
                }
                
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