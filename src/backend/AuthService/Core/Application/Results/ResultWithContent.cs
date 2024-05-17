using System.Text.Json.Serialization;
using Application.Abstractions.Results;
using Application.Common.Errors;
using Newtonsoft.Json;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace Application.Results;

public class ResultWithContent<T> : IResult
{
    public bool IsSuccess { get; set; }
    public Error? Error { get; set; }
    internal T? ResultData { get; set; }
    
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