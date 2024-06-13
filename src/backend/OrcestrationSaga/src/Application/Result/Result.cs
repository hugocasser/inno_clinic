using Application.Abstractions;

namespace Application.Result;

public class Result : IResult
{
    public bool IsSuccess { get; }
    private object? _content = ResultMessages.NoContent;
    public object? GetContent()
    {
        return _content;
    }

    public T GetContent<T>()
    {
        return (T)_content;
    }

    public int GetStatusCode()
    {
        return IsSuccess ? 200 : 400;
    }

    public Result(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }
    
    public Result(bool isSuccess, object? content)
    {
        IsSuccess = isSuccess;
        _content = content;
    }
}