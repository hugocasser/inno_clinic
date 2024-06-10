using Application.Abstractions;
using Application.Abstractions.Services.Saga;
using Application.Result;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services;

public class OrchestratorService(IKeyedServiceProvider serviceProvider)
    : IOrchestratorService
{
    public async Task<IResult> StartExecuteAsync(ITransactionDto request, CancellationToken cancellationToken = default)
    {
        var handlersKeys = request.GetOrderedHandlersKeys();
        var handlers = new List<ITransactionComponentHandler>();
        var handlersResponses = new List<ITransactionResult>();

        foreach (var handler in handlersKeys.Select(serviceProvider
                     .GetRequiredKeyedService<ITransactionComponentHandler>))
        {
            var handlingResult = await handler.HandleAsync(request, cancellationToken);

            if (handlingResult.NeedRollback)
            {
                var rollbackResult = await TryRollbackAsync(handlers, cancellationToken);

                return rollbackResult;
            }

            handlers.Add(handler);
        }

        return ResultBuilder.Failure(ResultMessages.InternalServerError);
    }

    public async Task<IResult> TryRollbackAsync(List<ITransactionComponentHandler> handlers,
        CancellationToken cancellationToken = default)
    {
        var requiredFails = new List<ITransactionResult>();
        var notRequiredFails = new List<ITransactionResult>();

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
                continue;
            }

            if (handler.RollbackRequired)
            {
                requiredFails.Add(rollbackResult);
            }
            
            notRequiredFails.Add(rollbackResult);
        }

        return ResultBuilder.BuildFromRollbackFails(requiredFails, notRequiredFails);
    }
}