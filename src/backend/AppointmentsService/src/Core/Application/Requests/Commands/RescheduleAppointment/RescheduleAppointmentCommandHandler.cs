using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Services;
using Application.Resources;
using Application.Result;
using MediatR;

namespace Application.Requests.Commands.RescheduleAppointment;

public class RescheduleAppointmentCommandHandler(
    ITransactionsProvider transactionsProvider,
    IAppointmentsRepository appointmentsRepository,
    IServicesService servicesService)
    : IRequestHandler<RescheduleAppointmentCommand, OperationResult>
{
    public async Task<OperationResult> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
    {
        transactionsProvider.StartTransaction();

        var appointment = await appointmentsRepository
            .GetAsync(request.AppointmentId, cancellationToken);
        
        if (appointment == null)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.AppointmentNotFound);
        }

        var serviceTimeResult = await servicesService
            .GetServiceTime(appointment.ServiceId, cancellationToken: cancellationToken);
        
        if (!serviceTimeResult.IsSuccess)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.TimeIsNotFree);
        }
        
        var newDate = DateOnly.FromDateTime(request.NewDate.Date);
        var newTime = TimeOnly.FromDateTime(request.NewDate.Date);
        var isTimeFree = await appointmentsRepository.IsTimeFreeAsync(appointment.ServiceId,
            newDate, newTime, serviceTimeResult.GetData(), cancellationToken);
        
        if (!isTimeFree)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.TimeIsNotFree);
        }
        
        var result = await appointmentsRepository
            .UpdateDateTimeAsync(request.AppointmentId, newDate, newTime, cancellationToken);
        
        if (result != 1)
        {
            transactionsProvider.Rollback();

            return ResultBuilder.InternalServerError();
        }
        
        transactionsProvider.Commit();
        
        return ResultBuilder.NoContent();
    }
}