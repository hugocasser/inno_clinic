using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Result;

namespace Application.TransactionComponents.CheckOfficeComponent;

public class CheckOfficeComponentHandler(IOfficesService officesService) : ITransactionComponentHandler
{
    public const string HandlerKey = "bb9f21f7-dce5-49a6-91ae-738d68032a2c";
    public bool RollbackRequired { get; } = false;
    public bool NeedRollback { get; } = false;

    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithOfficeId transaction)
        {
            return ResultBuilder.TransactionNoContent();
        }
        
        var checkResult = await officesService.IsOfficeExistAsync(transaction.OfficeId, cancellationToken);
        
        return checkResult 
            ? ResultBuilder.TransactionSuccess()
            : ResultBuilder.TransactionFailed();
    }

    public Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResultBuilder.TransactionNoContent());
    }
}