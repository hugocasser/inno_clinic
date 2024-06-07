using MediatR;

namespace Application.Abstractions;

public interface IRequestWithAsyncValidation<out T> : IRequest<T>;