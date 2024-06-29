using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Repositories;
using Application.Resources;
using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointmentResult;

public class CreateAppointmentResultCommandHandler(
    IAppointmentsRepository appointmentsRepository,
    IResultsRepository resultsRepository,
    ITransactionsProvider transactionsProvider)
    : IRequestHandler<CreateAppointmentResultCommand, OperationResult>
{
    public async Task<OperationResult> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken)
    {
        transactionsProvider.StartTransaction();
        
        var isExist = await appointmentsRepository
            .IsExistAsync(request.AppointmentId, cancellationToken);
        
        if (!isExist)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.AppointmentNotFound);
        }
        
        var result = await resultsRepository
            .IsExistAsync(request.AppointmentId, cancellationToken);
        
        if (result)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.ResultAlreadyExist);
        }
        
        var resultToCreate = request.MapToResult();
        
        var resultCreated = await resultsRepository
            .AddAsync(resultToCreate, cancellationToken);
        
        if (resultCreated != 1)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.InternalServerError();
        }
        
        transactionsProvider.Commit();
        
        return ResultBuilder.NoContent();
    }
}