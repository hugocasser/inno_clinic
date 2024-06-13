using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Result;

namespace Application.TransactionComponents.UpdateProfileComponent;

public class UpdateProfileComponentHandler
    (IProfilesService profilesService)
    : ITransactionComponentHandler
{
    public const string HandlerKey = "b7a6e32a-0a7d-4e8b-8d6a-8a6a8a6a8a6a";
    public bool RollbackRequired { get; } = true;
    public bool NeedRollback { get; } = true;
    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithProfileUpdating transaction)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        var result = await profilesService.UpdatePatientsProfileAsync(transaction, cancellationToken);
        
        if (!result.IsSuccess)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        transaction.SetPhotoId(result.GetContent<Guid?>());
        
        return ResultBuilder.TransactionSuccess();
    }

    public Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResultBuilder.TransactionSuccess());
    }
}