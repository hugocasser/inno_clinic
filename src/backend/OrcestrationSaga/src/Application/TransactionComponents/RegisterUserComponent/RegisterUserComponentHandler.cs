using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Result;

namespace Application.TransactionComponents.RegisterUserComponent;

public class RegisterUserComponentHandler(IAuthService authService) : ITransactionComponentHandler
{
    public const string HandlerKey = "e442d825-b7cc-4b63-9cc4-96f6f77f8eb8";
    public bool RollbackRequired { get; } = true;
    private Guid UserId { get; set; } = Guid.Empty;
    public bool NeedRollback { get; } = true;
    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithUserRegistration transaction)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        var registerResult = await authService
            .CreateAccountAsync(transaction.Email, transaction.Password, transaction.Role, cancellationToken);

        if (!registerResult.IsSuccess)
        {
            return ResultBuilder.TransactionNoContent();
        }

        UserId = registerResult.GetContent<Guid>();
            
        return ResultBuilder.TransactionSuccess();
    }

    public async Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        if (UserId == Guid.Empty)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        var rollbackResult = await authService
            .TryRollbackAsync(UserId, cancellationToken);
        
        return !rollbackResult.IsSuccess 
            ? ResultBuilder.TransactionFailed()
            : ResultBuilder.TransactionSuccess();
    }
}