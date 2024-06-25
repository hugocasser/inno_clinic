using Application.Abstractions;

namespace Application.Results;

public class Result : IResult
{
    public bool IsSuccess { get; init; } = true;
    private object? Data { get; set; }

    public object? GetResultData()
    {
        return Data;
    }
    
    public void SetResultData(object? data)
    {
        Data = data;
    }
    
    public void SetResultData<T>(T? data) where T : class 
    {
        Data = data;
    }
    
    public T? GetResultData<T>() where T : class 
    {
        return Data as T;
    }
}