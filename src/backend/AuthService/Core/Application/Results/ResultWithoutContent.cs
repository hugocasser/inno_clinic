using Application.Abstractions.Results;
using Application.Common.Errors;

namespace Application.Results;

public class ResultWithoutContent : IResult
{
    public bool IsSuccess { get; set; }
    public Error? Error { get; set; }
    public string GetResultMessage()
    {
        return !IsSuccess ? Error.Message : "No content";
    }

    public int? GetStatusCode()
    {
        return !IsSuccess ? Error.Code : 204;
    }

    public static ResultWithoutContent Success()
    {
        return new ResultWithoutContent();
    }

    public static ResultWithoutContent Failure(Error error)
    {
        return new ResultWithoutContent(error: error);
    }

    private ResultWithoutContent(Error? error = default)
    {
        if (error is not null)
        {
            Error = error;
            IsSuccess = false;
        }
        else
        {
            IsSuccess = true;   
        }
    }
}