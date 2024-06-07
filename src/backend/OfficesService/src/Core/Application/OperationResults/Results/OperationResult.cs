using Application.Abstractions.OperationResult;

namespace Application.OperationResults.Results;

public class OperationResult(bool isSuccess) : IResult
{
    public bool IsSuccess { get; private set; } = isSuccess;
    public object? ResultData { get; set; }
    public int StatusCode { get; set; } = 0;
    public object? GetOperationResult()
    {
        if (ResultData is not null)
        {
            return ResultData;
        }
        return IsSuccess ? "No content" : "Unprocessed error";
    }

    public int GetStatusCode()
    {
        if (StatusCode != 0)
        {
            return StatusCode;
        }
        
        return IsSuccess ? 204 : 500;
    }
}