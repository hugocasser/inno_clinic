using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Result;

namespace Application.TransactionComponents.CreateProfileComponent;

public class CreateProfileComponentHandler(IProfilesService profilesService) : ITransactionComponentHandler 
{
    public const string HandlerKey = "df72774f-c643-469c-a7ea-35735faf8c07";
    public bool RollbackRequired { get; } = true;
    public bool NeedRollback { get; } = true;
    private Guid DoctorId { get; set; } = Guid.Empty;
    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithProfileCreation transaction)
        {
            return ResultBuilder.TransactionFailed();    
        }
        
        var profile = await profilesService.CreateProfileAsync(transaction,cancellationToken);
        
        if (!profile.IsSuccess)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        DoctorId = profile.GetContent<Guid>();
        
        return ResultBuilder.TransactionSuccess();
    }

    public async Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        var rollbackResult = await profilesService.TryRollbackAccountAsync(DoctorId, cancellationToken);
        
        return rollbackResult.IsSuccess
            ? ResultBuilder.TransactionSuccess()
            : ResultBuilder.TransactionFailed();
    }
}