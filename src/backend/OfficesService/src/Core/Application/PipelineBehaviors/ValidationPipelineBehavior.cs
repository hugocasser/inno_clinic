using Application.Abstractions;
using Application.Abstractions.OperationResult;
using Application.OperationResults;
using FluentValidation;
using FluentValidation.Results;
using MediatR;

namespace Application.PipelineBehaviors;

public class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> _validators)
    : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<IResult> where TResponse : IResult
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);

        ValidationFailure[] failures;
        
        if (request is IRequestWithAsyncValidation<IResult>)
        {
            var validationTasks = _validators
                .Select(async validator => await validator.ValidateAsync(context, cancellationToken));
            
            var results = await Task.WhenAll(validationTasks);
            
            failures = results
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToArray();
        }
        else
        {
            failures = _validators
                .Select(validator => validator.Validate(context))
                .SelectMany(result => result.Errors)
                .Where(failure => failure != null)
                .ToArray();
        }
        
        if (failures.Length == 0)
        {
            return await next();
        }

        var errorMessages = failures
            .Select(x => x.ErrorMessage)
            .ToArray();

        var result = (TResponse)ResultBuilder.BadRequest(string.Join(", ", errorMessages));

        return result;
    }
}