namespace Presentation.Middleware;

public class CustomExceptionHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
        {
            await HandleExceptionAsync(context, exception);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var result = "Error occurred during request processing" + '\n' + exception.Message;
        
        context.Response.ContentType = "application/json";
            
        return context.Response.WriteAsync(result);
    }
}