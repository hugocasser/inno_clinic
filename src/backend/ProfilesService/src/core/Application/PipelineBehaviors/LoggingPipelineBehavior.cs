using Application.Abstractions.CQRS;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;
using Microsoft.Extensions.Logging;

namespace Application.PipelineBehaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    (ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> where TResponse : HttpRequestResult
{
    public Task<TResponse> ExecuteBeforeRequestHandlingAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Request started: {@RequestName}, \n DateTime: {@DateTime}",
            nameof(TRequest), DateTime.UtcNow);

        var result = HttpResultBuilder.NoContent();
       
        return Task.FromResult(result as TResponse)!;
    }

    public Task<TResponse> ExecuteAfterRequestHandlingAsync(TRequest? request = default, TResponse? response = default,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Request completed: {@RequestName}, \n DateTime: {@DateTime}",
            nameof(TRequest), DateTime.UtcNow);
        
        var result = HttpResultBuilder.NoContent();
       
        return Task.FromResult(result as TResponse)!;
    }
}