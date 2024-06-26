using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Services;
using Application.Resources;
using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler
    (IAppointmentsRepository appointmentsRepository,
        IProfilesService profilesService,
        IServicesService servicesService) : IRequestHandler<CreateAppointmentCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        var isPatientExist = await profilesService.IsPatientExist(request.PatientId, cancellationToken);

        if (!isPatientExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.PatientNotFound);
        }
        
        var isServiceExist = await servicesService.IsServiceExist(request.ServiceId, cancellationToken);
        
        if (!isServiceExist)
        {
            return ResultBuilder.BadRequest(RespounseMessages.ServiceNotFound);
        }
        
        var isDoctorInOffice = await profilesService.IsDoctorInOffice(request.DoctorId, request.OfficeId, cancellationToken);
        
        if (!isDoctorInOffice)
        {
            return ResultBuilder.BadRequest(RespounseMessages.DoctorIsNotInOffice);
        }
        
        var isDoctorInSpecialization = await profilesService
            .IsDoctorInSpecializationExist(request.DoctorId, request.ServiceId, cancellationToken);
        
        if (!isDoctorInSpecialization)
        {
            return ResultBuilder.BadRequest(RespounseMessages.DoctorDoNotHaveThisSpecialization);
        }

        var appointment = request.MapToAppointment();
        
        appointmentsRepository.StartTransaction();
        
        var result = await appointmentsRepository.AddAsync(appointment, cancellationToken);

        if (result != 1)
        {
            appointmentsRepository.Rollback();
            
            return ResultBuilder.InternalServerError();
        }
        
        appointmentsRepository.Commit();
        
        return ResultBuilder.NoContent();
    }
}