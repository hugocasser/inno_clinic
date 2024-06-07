using Application.Abstractions.OperationResult;
using Application.OperationResults.Results;

namespace Application.OperationResults;

public static class ResultBuilder
{
    public static IResult Success()
    {
        return new OperationResult(true);
    }
    
    public static IResult Failure()
    {
        return new OperationResult(false);
    }
    
    public static IResult WithData(this IResult result, object? data)
    {
        result.ResultData = data;
        
        return result;
    }

    public static IResult WithStatusCode(this IResult result, int statusCode)
    {
        result.StatusCode = statusCode;
        
        return result;
    }
    
    public static IResult Unauthorized(string? message)
    {
        return new OperationResult(false).WithStatusCode(401).WithData(message);
    }
    
    public static IResult Forbidden(string? message)
    {
        return new OperationResult(false).WithStatusCode(403).WithData(message);
    }
    
    public static IResult NotFound(string? message)
    {
        return new OperationResult(false).WithStatusCode(404).WithData(message);
    }
    
    public static IResult InternalServerError(string? message)
    {
        return new OperationResult(false).WithStatusCode(500).WithData(message);
    }
    
    public static IResult BadRequest(string? message)
    {
        return new OperationResult(false).WithStatusCode(400).WithData(message);
    }
}