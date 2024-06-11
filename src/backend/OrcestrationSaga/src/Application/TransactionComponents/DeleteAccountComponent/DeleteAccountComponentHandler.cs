using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Result;

namespace Application.TransactionComponents.DeleteAccountComponent;

public class DeleteAccountComponentHandler(IAuthService authService) : ITransactionComponentHandler
{
    public const string HandlerKey = "a6a5b4f9-5f8a-4b9e-8b9f-5b9f5b9f5b9f";
    public bool RollbackRequired { get; } = true;
    public bool NeedRollback { get; } = true;
    private Guid _accountId = Guid.Empty;
    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithAccountDeleting transaction)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        var result = await authService.DeleteAccountAsync(transaction.AccountId, cancellationToken);
        
        if (!result.IsSuccess)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        _accountId = transaction.AccountId;
        
        return ResultBuilder.TransactionSuccess();
    }

    public async Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_accountId == Guid.Empty)
        {
            return ResultBuilder.TransactionSuccess();
        }
        
        var result = await authService.TryRollbackAsync(_accountId, cancellationToken);
        
        if (!result.IsSuccess)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        return ResultBuilder.TransactionSuccess();
    }
}