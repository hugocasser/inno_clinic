using Application.Abstractions.Services;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Auth;

public class HttpContextAccessorExtension : HttpContextAccessor, IHttpContextAccessorExtension
{
    public string GetUserId()
    {
        if (IsContextNull() || HttpContext.User.Identities.First().Claims.FirstOrDefault()?.Value is null ||
            HttpContext.User.Identities.First().Claims.FirstOrDefault()?.Value == string.Empty)
        {
            return Guid.Empty.ToString();
        }

        return HttpContext.User.Identities.First().Claims.FirstOrDefault()?.Value;
    }

    private bool IsContextNull()
    {
        return HttpContext is null;
    }
}