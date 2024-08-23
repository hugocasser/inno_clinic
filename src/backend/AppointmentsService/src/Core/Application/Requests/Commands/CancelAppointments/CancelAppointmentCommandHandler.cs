using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Repositories;
using Application.Resources;
using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CancelAppointments;

public class CancelAppointmentCommandHandler
    (IAppointmentsRepository appointmentsRepository,
     ITransactionsProvider transactionsProvider)
    : IRequestHandler<CancelAppointmentCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        transactionsProvider.StartTransaction();
        
        var isExist = await appointmentsRepository
            .IsExistAsync(request.Id, cancellationToken);
        
        if (!isExist)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.AppointmentNotFound);
        }
        
        var result = await appointmentsRepository.DeleteAsync(request.Id, cancellationToken);
        
        if (result != 1)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.InternalServerError();
        }
        
        transactionsProvider.Commit();
        
        return ResultBuilder.NoContent();
    }
}