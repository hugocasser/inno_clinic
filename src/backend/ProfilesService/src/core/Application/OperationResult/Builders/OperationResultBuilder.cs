using Application.OperationResult.Errors;
using Application.OperationResult.Results;

namespace Application.OperationResult.Builders;

public static class OperationResultBuilder
{
    public static OperationResult<T> Success<T>(T content)
    {
        return new OperationResult<T>(content);
    }
    
    public static OperationResult<bool> Success()
    {
        return new OperationResult<bool>(true);
    }
    
    public static OperationResult<bool> Failure()
    {
        return new OperationResult<bool>(false);
    }
    
    public static OperationResult<bool> Failure(params Error[] errors)
    {
        return new OperationResult<bool>(errors);
    }
    
    public static OperationResult<T> Failure<T>(params Error[] errors)
    {
        return new OperationResult<T>(errors);
    }
}