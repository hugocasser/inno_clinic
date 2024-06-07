using Application.Abstractions.Http;
using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Services.Http;

public class HttpContextAccessorExtensions : HttpContextAccessor, IHttpContextAccessorExtensions
{
    public Guid GetUserIdFromClaims()
    {
        if (HttpContext?.User.Identities.First().Claims.FirstOrDefault()?.Value is null ||
            HttpContext.User.Identities.First().Claims.FirstOrDefault()?.Value == string.Empty)
        {
            return Guid.Empty;
        }

        var value = HttpContext.User.Identities.First().Claims.FirstOrDefault()?.Value;

        return Guid.TryParse(value, out var id) ? id : Guid.Empty;
    }

    public bool CheckUserRoles(EnumRoles roleToCheck = EnumRoles.Receptionist)
    {
        if (HttpContext?.User.Identities.First().Claims.FirstOrDefault()?.Value is null ||
            HttpContext.User.Identities.First().Claims.FirstOrDefault()?.Value == string.Empty)
        {
            return false;
        }
        
        var claims = HttpContext.User.Identities.First().Claims;
        
        return claims.Where(claim => claim.ValueType == "role:")
            .Any(role => role.Value == roleToCheck.ToString());
    }
}