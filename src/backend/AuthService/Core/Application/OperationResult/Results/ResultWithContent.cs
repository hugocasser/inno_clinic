using Application.Abstractions.Results;
using Application.OperationResult.Errors;
using Newtonsoft.Json;

namespace Application.OperationResult.Results;

public class ResultWithContent<T> : IResult
{
    public bool IsSuccess { get; set; }
    public Error? Error { get; set; }
    public T? ResultData { get; set; }
    
    public object? GetResultMessage()
    {
        if (!IsSuccess)
        {
            return Error.Message;
        }

        return ResultData;
    }

    public int? GetStatusCode()
    {
        return !IsSuccess ? Error.Code : 200;
    }

    public static ResultWithContent<T> Success(T resultData)
    {
        return new ResultWithContent<T>(resultData: resultData);
    }
    
    public static ResultWithContent<T> Failure(Error error)
    {
        return new ResultWithContent<T>(error: error);
    }

    private ResultWithContent(Error? error = default, T? resultData = default)
    {
        if (error is not null)
        {
            IsSuccess = false;
            Error = error;
        }
        else
        {
            IsSuccess = true;
            ResultData = resultData;
        }
    }
}