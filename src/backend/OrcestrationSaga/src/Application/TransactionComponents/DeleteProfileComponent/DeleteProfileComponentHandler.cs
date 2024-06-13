using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Dtos.Responses;
using Application.Result;

namespace Application.TransactionComponents.DeleteProfileComponent;

public class DeleteProfileComponentHandler(IProfilesService profilesService) : ITransactionComponentHandler
{
    public const string HandlerKey = "97a79371-3b90-4f62-a549-dfad80d5fbcd";
    public bool RollbackRequired { get; } = true;
    public bool NeedRollback { get; } = true;
    
    private Guid _profileId = Guid.Empty;
    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithProfileDeleting transaction)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        var result = await profilesService.DeleteProfileAsync(transaction.ProfileId, cancellationToken);
        
        if (!result.IsSuccess)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        transaction.SetAccountId(result.GetContent<DeleteProfileResponse>()!.AccountId);
        transaction.SetPhotoId(result.GetContent<DeleteProfileResponse>()!.PhotoId);
        
        _profileId = transaction.ProfileId;
        
        return ResultBuilder.TransactionSuccess();
    }

    public async Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_profileId == Guid.Empty)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        var result = await profilesService.TryRollbackAccountAsync(_profileId, cancellationToken);
        
        return !result.IsSuccess 
            ? ResultBuilder.TransactionFailed() 
            : ResultBuilder.TransactionSuccess();
    }
}