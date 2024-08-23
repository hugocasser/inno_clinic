using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Repositories;
using Application.Resources;
using Application.Result;
using MediatR;

namespace Application.Requests.Commands.ApproveAppointment;

public class ApproveAppointmentCommandHandler(
    IAppointmentsRepository appointmentsRepository,
    ITransactionsProvider transactionsProvider)
    : IRequestHandler<ApproveAppointmentCommand, OperationResult>
{
    public async Task<OperationResult> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        transactionsProvider.StartTransaction();
        
        var isExist = await appointmentsRepository.IsExistAsync(request.Id, cancellationToken);
        
        if (!isExist)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.AppointmentNotFound);
        }
        
        var result = await appointmentsRepository.ApproveAsync(request.Id, cancellationToken);
        
        if (result != 1)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.InternalServerError();
        }
        
        transactionsProvider.Commit();
        
        return ResultBuilder.NoContent();
    }
}