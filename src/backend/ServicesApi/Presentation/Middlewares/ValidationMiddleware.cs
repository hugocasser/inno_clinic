using BLL.Abstractions;

namespace Presentation.Middlewares;

public class ValidationMiddleware(RequestDelegate next)
{
    public Task InvokeAsync(HttpContext context)
    {
        foreach (var item in context.Items)
        {
            if (item.Value is not IRequestDto requestDto) continue;

            var validationResult = requestDto.Validate();

            if (validationResult.IsValid) continue;

            context.Response.StatusCode = 400;
                    
            return context.Response.WriteAsync(validationResult.ToString());
        }
        
        return next(context);
    }
}