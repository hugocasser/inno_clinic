namespace Application.Abstractions.CQRS;

public interface IRequestHandler<in TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    public Task<TResponse> HandleAsync(TRequest request, CancellationToken cancellationToken = default);
}