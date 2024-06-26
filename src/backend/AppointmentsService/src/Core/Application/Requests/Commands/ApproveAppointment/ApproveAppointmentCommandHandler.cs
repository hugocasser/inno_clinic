using Application.Abstractions.Persistence.Repositories;
using Application.Resources;
using Application.Result;
using MediatR;

namespace Application.Requests.Commands.ApproveAppointment;

public class ApproveAppointmentCommandHandler(IAppointmentsRepository appointmentsRepository)
    : IRequestHandler<ApproveAppointmentCommand, OperationResult>
{
    public async Task<OperationResult> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        appointmentsRepository.StartTransaction();
        
        var isExist = await appointmentsRepository.IsExistAsync(request.Id, cancellationToken);
        
        if (!isExist)
        {
            appointmentsRepository.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.AppointmentNotFound);
        }
        
        var result = await appointmentsRepository.ApproveAsync(request.Id, cancellationToken);
        
        if (result != 1)
        {
            appointmentsRepository.Rollback();
            
            return ResultBuilder.InternalServerError();
        }
        
        appointmentsRepository.Commit();
        
        return ResultBuilder.NoContent();
    }
}