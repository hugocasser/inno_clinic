using Application.Abstractions.OperationResult;

namespace Application.Abstractions.CQRS;
public interface IPipelineBehavior<in TRequest, in TResponse> where TRequest : IRequest<TResponse> where TResponse : notnull
{
    public Task<IResult> ExecuteBeforeRequestHandlingAsync(TRequest request, CancellationToken cancellationToken = default);
    
    public Task<IResult> ExecuteAfterRequestHandlingAsync(TRequest? request = default, TResponse? response = default, CancellationToken cancellationToken = default);
}