using Application.Abstractions.Results;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using FluentValidation;
using MediatR;

namespace Application.PipelineBehaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<IResult> where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var failures = _validators
            .Select(validator => validator.Validate(context))
            .SelectMany(result => result.Errors)
            .Where(failure => failure != null)
            .ToArray();
        
        if (failures.Length != 0)
        {
            ResultWithoutContent.Failure(Error.BadRequest().WithMessage(failures));
        }
        
        return await next();
    }
}