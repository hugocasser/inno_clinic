using System.Text;
using FluentValidation.Results;

namespace Application.OperationResult.Errors;

public class Error
{
    public int Code { get; private set; }
    public string Message { get; private set; } = null!;

    public static Error BadRequest()
    {
        return new Error(400);
    }
    public static Error Unauthorized()
    {
        return new Error(401);
    }
    
    public static Error Forbidden()
    {
        return new Error(403);
    }
    
    public static Error NotFound(string message = "Not Found")
    {
        return new Error(404);
    }
    
    public static Error InternalServerError()
    {
        return new Error(500);
    }
    
    public Error WithMessage(string message)
    {
        Message = message;

        return this;
    }

    public Error WithMessage(IEnumerable<ValidationFailure> validationFailures)
    {
        var builder = new StringBuilder();

        foreach (var validationFailure in validationFailures)
        {
            builder.Append(validationFailure.ErrorMessage + "\n");
        }
        
        Message = builder.ToString();
        
        return this;
    }

    private Error(int code, string message)
    {
        Code = code;
        Message = message;
    }

    private Error(int code)
    {
        Code = code;
    }
}