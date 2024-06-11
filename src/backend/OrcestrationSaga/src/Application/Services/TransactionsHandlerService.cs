using Application.Abstractions;
using Application.Abstractions.Services.Saga;
using Application.Result;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public class TransactionsHandlerService
    (IKeyedServiceProvider serviceProvider,
        ITransactionsNotifierService transactionsNotifierService)
    : ITransactionsHandlerService
{
    public async Task<IResult> StartExecuteAsync(ITransactionDto request, CancellationToken cancellationToken = default)
    {
        var isHandlersSuccess = true;
        var handlersKeys = request.GetHandlersKeys();
        var handlers = new List<ITransactionComponentHandler>();
        var handlersSuccessResponses = new List<ITransactionResult>();

        foreach (var handler in handlersKeys.Select(serviceProvider
                     .GetRequiredKeyedService<ITransactionComponentHandler>))
        {
            var handlingResult = await handler.HandleAsync(request, cancellationToken);

            if (!handlingResult.IsSuccess)
            {
                isHandlersSuccess = false;
                
                var rollbackResult = await TryRollbackAsync(handlers, cancellationToken);

                return rollbackResult;
            }

            handlers.Add(handler);
            handlersSuccessResponses.Add(handlingResult);
        }

        await transactionsNotifierService.NotifyAsync(handlersSuccessResponses, cancellationToken: cancellationToken);
        
        return isHandlersSuccess 
            ? ResultBuilder.BuildFromHandlersSuccess(handlersSuccessResponses)
            : ResultBuilder.Failure(ResultMessages.InternalServerError);
    }

    public async Task<IResult> TryRollbackAsync(List<ITransactionComponentHandler> handlers,
        CancellationToken cancellationToken = default)
    {
        var requiredFails = new List<ITransactionResult>();
        var notRequiredFails = new List<ITransactionResult>();
        var rollbackResults = new List<ITransactionResult>();
        
        var isRollbackSuccess = true;
        
        for (var i = handlers.Count - 1; i >= 0; i--)
        {
            var handler = handlers[i];

            if (!handler.NeedRollback)
            {
                continue;
            }

            var rollbackResult = await handler.TryRollbackAsync(cancellationToken);

            if (rollbackResult.IsSuccess)
            {
                rollbackResults.Add(rollbackResult);
                continue;
            }
            
            if (handler.RollbackRequired)
            {
                isRollbackSuccess = false;
                requiredFails.Add(rollbackResult);
            }
            else
            {
                notRequiredFails.Add(rollbackResult);
            }
        }
        
        await transactionsNotifierService.NotifyAsync(requiredFails, notRequiredFails, cancellationToken);

        return isRollbackSuccess 
            ? ResultBuilder.BuildFromRollbackSuccess(rollbackResults)
            : ResultBuilder.BuildFromRollbackFails(requiredFails, notRequiredFails);
    }
}