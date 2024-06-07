using Application.OperationResult.Errors;
using Application.OperationResult.Results;

namespace Application.OperationResult.Builders;

public static class OperationResultBuilder
{
    public static OperationResult<T> Success<T>(T content)
    {
        return new OperationResult<T>(content);
    }
    
    public static OperationResult<T> Failure<T>(params Error[] errors)
    {
        return new OperationResult<T>(errors);
    }
}