using Application.Abstractions.Persistence;
using Application.Abstractions.Persistence.Repositories;
using Application.Resources;
using Application.Result;
using MediatR;

namespace Application.Requests.Commands.EditResult;

public class EditResultCommandHandler(
    IResultsRepository resultsRepository,
    ITransactionsProvider transactionsProvider)
    : IRequestHandler<EditResultCommand, OperationResult>
{
    public async Task<OperationResult> Handle(EditResultCommand request, CancellationToken cancellationToken)
    {
        transactionsProvider.StartTransaction();
        
        
        var isExist = await resultsRepository
            .IsExistAsync(request.Id, cancellationToken);
        
        if (!isExist)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.BadRequest(RespounseMessages.ResultNotFound);
        }
        
        
        var result = await resultsRepository
            .UpdateAsync(request.Id, request.Complaints, request.Conclusion, request.Recommendation, cancellationToken);
        
        if (result != 1)
        {
            transactionsProvider.Rollback();
            
            return ResultBuilder.InternalServerError();
        }
        
        transactionsProvider.Commit();
        
        return ResultBuilder.Ok();
    }
}