namespace Application.Common.Errors;

public class Error
{
    public int Code { get; private set; }
    public string Message { get; private set; } = null!;

    public static Error NotFound(string message = "Not Found")
    {
        return new Error(404);
    }

    public static Error BadRequest()
    {
        return new Error(400);
    }

    public static Error InternalServerError()
    {
        return new Error(500);
    }

    public static Error Unauthorized()
    {
        return new Error(401);
    }
    
    public Error WithMessage(string message)
    {
        Message = message;

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