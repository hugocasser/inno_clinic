using Application.Abstractions.Services;
using Application.Abstractions.Services.Saga;
using Microsoft.Extensions.Logging;

namespace Application.Services.Notifications;

public class TransactionsNotifierService(ILogger<TransactionsNotifierService> logger, INotifierService notifierService) : ITransactionsNotifierService
{
    public async Task NotifyAsync(List<ITransactionResult> requiredFails, List<ITransactionResult> notRequiredFails, CancellationToken cancellationToken = default)
    {
        logger.LogWarning("Not required components rollback failed: \n");
        
        foreach (var fail in notRequiredFails)
        {
            logger.LogWarning
                ("Transaction component {componentName} with transaction id {TransactionId} not required rollback \n",
                    fail.TransactionId, fail.ComponentName);
        }
        
        logger.LogCritical("Required components rollback failed: \n");
        
        foreach (var fail in requiredFails)
        {
            logger.LogCritical
                ("Transaction component {componentName} with transaction id {TransactionId} required rollback \n",
                    fail.TransactionId, fail.ComponentName);
            
            notifierService.AddMessageToNotify
                ($"Transaction component {fail.ComponentName} with transaction id {fail.TransactionId} required rollback");
        }
        
        await notifierService.NotifyAsync(cancellationToken);
    }

    public async Task NotifyAsync(List<ITransactionResult> successResults, CancellationToken cancellationToken = default)
    {
        foreach (var success in successResults)
        {
            logger.LogInformation
                ("Transaction component {componentName} with transaction id {TransactionId} success rollback \n",
                    success.TransactionId, success.ComponentName);
        }
        
        await notifierService.NotifyAsync(cancellationToken);
    }
}