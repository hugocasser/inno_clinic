using Application.Abstractions.CQRS;
using Application.Abstractions.OperationResult;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Services.CQRS;

public class RequestSender(IServiceProvider serviceProvider) : IRequestSender
{
    public async Task<IResult> SendAsync<TRequest,TResponse>(TRequest request,
        CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse> where TResponse : IResult
    {
        var pipelineBehaviors = serviceProvider
            .GetServices<IPipelineBehavior<IRequest<TResponse>, TResponse>>().ToList();

        foreach (var pipelineBehavior in pipelineBehaviors)
        {
            var executionResult = await pipelineBehavior.ExecuteBeforeRequestHandlingAsync(request, cancellationToken);

            if (!executionResult.IsSuccess)
            {
                return executionResult;
            }
        }
        
        var handler = serviceProvider.GetRequiredService<IRequestHandler<TRequest, TResponse>>();
        
        var requestResult = await handler.HandleAsync(request, cancellationToken);

        foreach (var pipelineBehavior in pipelineBehaviors)
        {
            var executionResult = await pipelineBehavior.ExecuteAfterRequestHandlingAsync(request, requestResult, cancellationToken);
            if (!executionResult.IsSuccess)
            {
                return executionResult;
            }
        }

        return requestResult;
    }
}