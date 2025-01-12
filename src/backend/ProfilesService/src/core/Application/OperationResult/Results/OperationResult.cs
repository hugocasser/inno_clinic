using System.Text;
using Application.Abstractions.OperationResult;
using Application.OperationResult.Errors;

namespace Application.OperationResult.Results;

public class OperationResult<T> : IResult
{
    public bool IsSuccess { get; set; }
    private T? Content { get; set; }
    private IEnumerable<Error>? Errors { get; set; }

    public OperationResult(Error[] errors)
    {
        IsSuccess = false;
        Errors = errors;
    }
    
    public OperationResult(T content)
    {
        IsSuccess = true;
        Content = content;
    }
    
    public T GetTypedContent()
    {
        if (Content != null)
        {
            return Content;
        }
        
        throw new InvalidOperationException("Content is null");
    }

    public string ErrorsToString()
    {
        if (Errors is null)
        {
            return string.Empty;
        }

        var stringBuilder = new  StringBuilder();
        
        foreach (var error in Errors)
        {
            stringBuilder.Append(error.Message);
            stringBuilder.Append(',');
        }
        
        return stringBuilder.ToString();
    }
}