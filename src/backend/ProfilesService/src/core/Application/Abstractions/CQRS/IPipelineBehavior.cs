using Application.Abstractions.OperationResult;
using Application.OperationResult.Results;

namespace Application.Abstractions.CQRS;
public interface IPipelineBehavior<in TRequest, TResponse> where TRequest : IRequest<TResponse> where TResponse : notnull
{
    public Task<TResponse> ExecuteBeforeRequestHandlingAsync(TRequest request, CancellationToken cancellationToken = default);
    
    public Task<TResponse> ExecuteAfterRequestHandlingAsync(TRequest? request = default, TResponse? response = default, CancellationToken cancellationToken = default);
}