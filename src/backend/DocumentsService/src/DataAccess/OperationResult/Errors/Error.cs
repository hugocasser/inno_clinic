namespace DataAccess.OperationResult.Errors;

public class Error(string message)
{
    public string Message { get; } = message;
}