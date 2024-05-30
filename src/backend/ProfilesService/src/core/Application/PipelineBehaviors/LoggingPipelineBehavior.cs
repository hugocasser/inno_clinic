using Application.Abstractions.CQRS;
using Application.Abstractions.OperationResult;
using Application.OperationResult.Builders;
using Microsoft.Extensions.Logging;

namespace Application.PipelineBehaviors;

public class LoggingPipelineBehavior<TRequest, TResponse>
    (ILogger<LoggingPipelineBehavior<TRequest, TResponse>> logger) 
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse> where TResponse : notnull
{
    public Task<IResult> ExecuteBeforeRequestHandlingAsync(TRequest request, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Request started: {@RequestName}, \n DateTime: {@DateTime}",
            nameof(TRequest), DateTime.UtcNow);

        var result = OperationResultBuilder.Success() as IResult;
       
        return Task.FromResult(result);
    }

    public Task<IResult> ExecuteAfterRequestHandlingAsync(TRequest? request = default, TResponse? response = default,
        CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Request completed: {@RequestName}, \n DateTime: {@DateTime}",
            nameof(TRequest), DateTime.UtcNow);
        
        var result = OperationResultBuilder.Success() as IResult;
       
        return Task.FromResult(result);
    }
}