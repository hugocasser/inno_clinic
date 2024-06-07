namespace Application.OperationResult.Errors;

public class Error(string message)
{
    public string Message { get; set; } = message;

    protected Error(Exception exception) : this(exception.Message)
    {
    }
}