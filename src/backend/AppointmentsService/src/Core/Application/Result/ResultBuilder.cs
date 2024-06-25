using Application.Resources;

namespace Application.Result;

public static class ResultBuilder
{
    public static OperationResult NoContent()
    {
        return new OperationResult(true, RespounseMessages.NoContent);
    }
    
    public static OperationResult Ok(object? message = null)
    {
        return new OperationResult(true, message, 200);
    }
    
    public static OperationResult BadRequest(object? message = null)
    {
        return new OperationResult(false, message, 400);
    }
    
    public static OperationResult NotFound()
    {
        return new OperationResult(false, RespounseMessages.NotFound, 404);
    }
    
    public static OperationResult InternalServerError()
    {
        return new OperationResult(false, RespounseMessages.InternalServerError, 500);
    }
}