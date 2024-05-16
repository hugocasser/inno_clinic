using System.Text.Json.Serialization;
using Application.Abstractions.Results;
using Application.Common.Errors;
using Newtonsoft.Json;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace Application.Results;

public class Result<T> : IResult
{
    public bool IsSuccess { get; set; }
    public Error? Error { get; set; }
    private T? ResultData { get; set; }
    
    public string GetResultMessage()
    {
        if (!IsSuccess)
        {
            return Error.Message;
        }

        var json = JsonConvert.SerializeObject(ResultData, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.Objects
        });

        return json;
    }
    
    public static Result<T> Success(T resultData)
    {
        return new Result<T>(resultData: resultData);
    }
    
    public static Result<T> Failure(Error error)
    {
        return new Result<T>(error: error);
    }

    private Result(Error? error = default, T? resultData = default)
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