using BLL.Resources;

namespace BLL.Result;

public static class ResultBuilder
{
    public static OperationResult Success()
    {
        return new OperationResult
        {
            IsSuccess = true,
            StatusCode = 204
        };
    }
    
    public static OperationResult Success(object message)
    {
        return new OperationResult
        {
            IsSuccess = true,
            Message = message,
            StatusCode = 200
        };
    }
    
    public static OperationResult UnexpectedError()
    {
        return new OperationResult
        {
            IsSuccess = false,
            Message = RespounseMessages.UnexpectedError,
            StatusCode = 500
        };
    }
    
    public static OperationResult NotFound(string message)
    {
        return new OperationResult
        {
            IsSuccess = false,
            Message = message,
            StatusCode = 404
        };
    }
    
    public static OperationResult BadRequest(string message)
    {
        return new OperationResult
        {
            IsSuccess = false,
            Message = message,
            StatusCode = 400
        };
    }
}