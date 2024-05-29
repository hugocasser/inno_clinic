using Application.Abstractions.OperationResult;

namespace Application.Abstractions.CQRS;

public interface IRequestSender
{
    public Task<IResult> SendAsync<TResponse>(IRequest<TResponse> request,
        CancellationToken cancellationToken = default) where TResponse : IResult;
}