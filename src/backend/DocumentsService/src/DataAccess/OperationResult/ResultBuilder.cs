using DataAccess.OperationResult.Abstractions;
using DataAccess.OperationResult.Errors;

namespace DataAccess.OperationResult;

public static class ResultBuilder
{
    public static IResult Success()
    {
        return new Results.OperationResult();
    }
    
    public static IResult Success(object content)
    {
        return new Results.OperationResult(content);
    }
    
    public static IResult Failure(params Error[] errors)
    {
        return new Results.OperationResult(errors);
    }
    
    public static IResult Failure(string message)
    {
        return new Results.OperationResult(new Error(message));
    }
    
    public static IResult Failure(Error error)
    {
        return new Results.OperationResult(error);
    }
    
    public static IResult Failure()
    {
        var result = new Results.OperationResult
        {
            IsSuccess = false
        };

        return result;
    }

    public static IResult Failure(ICollection<Error> errors)
    {
        return new Results.OperationResult(errors);
    }
}