namespace Application.Common.Errors;

public class Error
{
    public int Code { get; private set; }
    public string Message { get; private set; } = null!;

    public static Error NotFound(string message = "Not Found")
    {
        return new Error(404, message);
    }

    public static Error BadRequest(string message = "Bad Request")
    {
        return new Error(400, message);
    }

    public static Error InternalServerError(string message = "Internal Server Error")
    {
        return new Error(500, message);
    }

    public static Error Unauthorized(string message = "Unauthorized")
    {
        return new Error(401, message);
    }

    private Error(int code, string message)
    {
        Code = code;
        Message = message;
    }
}