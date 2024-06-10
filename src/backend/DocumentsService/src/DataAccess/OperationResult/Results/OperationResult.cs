using DataAccess.OperationResult.Abstractions;
using DataAccess.OperationResult.Errors;
using DataAccess.OperationResult.Resources;

namespace DataAccess.OperationResult.Results;

public class OperationResult : IResult
{
    public bool IsSuccess { get; set; }
    private object? Content { get; set; }
    private readonly ICollection<Error>? _errors = new List<Error>();
    public object? GetContent()
    {
        return IsSuccess ? Content : _errors;
    }
    
    public T GetTypedContent<T>()
    {
        if (IsSuccess && Content is T typedContent)
        {
            return typedContent;
        }

        throw new Exception("Can't get typed content");
    }
    
    public OperationResult(params Error[] errors)
    {
        IsSuccess = false;
        _errors = errors.ToList();
    }
    
    public OperationResult(object content)
    {
        IsSuccess = true;
        Content = content;
    }
    
    public OperationResult()
    {
        IsSuccess = true;
        Content = ResultsMessages.NoContent;
    }
}