using Application.Abstractions.OperationResult;
using Application.OperationResult.Errors;

namespace Application.OperationResult.Results;

public class OperationResult<T> : IResult
{
    public bool IsSuccess { get; set; }
    private T? Content { get; set; }

    private IEnumerable<Error>? Errors { get; set; }
     
    public OperationResult(bool isSuccess)
    {
        IsSuccess = isSuccess;
    }

    // public OperationResult(Error[] errors)
    // {
    //     IsSuccess = false;
    //     Errors = errors;
    // }
    
    public OperationResult(T content)
    {
        IsSuccess = true;
        Content = content;
    }
    
    public OperationResult(IEnumerable<Error> errors)
    {
        IsSuccess = false;
        Errors = errors;
    }
    
    public T GetContent()
    {
        if (Content != null)
        {
            return Content;
        }
        
        throw new InvalidOperationException("Content is null");
    }

    public IEnumerable<Error> GetErrors()
    {
        return Errors ?? Enumerable.Empty<Error>();
    }
}