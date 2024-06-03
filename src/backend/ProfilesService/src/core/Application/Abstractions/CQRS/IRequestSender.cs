using Application.Abstractions.OperationResult;

namespace Application.Abstractions.CQRS;

public interface IRequestSender
{
    public Task<IResult> SendAsync<TRequest, TResponse>(TRequest request,
        CancellationToken cancellationToken = default) where TRequest : IRequest<TResponse> where TResponse : IResult;
}