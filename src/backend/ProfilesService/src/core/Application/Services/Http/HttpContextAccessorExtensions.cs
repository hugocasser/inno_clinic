using System.Collections.Frozen;
using System.Collections.ObjectModel;
using Application.Abstractions.Http;
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

    public IReadOnlyCollection<string>  GetUserRoleFromClaims()
    {
        if (HttpContext?.User.Identities.First().Claims.FirstOrDefault()?.Value is null ||
            HttpContext.User.Identities.First().Claims.FirstOrDefault()?.Value == string.Empty)
        {
            return new List<string>()
            {
                "none"
                
            }.AsReadOnly();
        }
        
        var claims = HttpContext.User.Identities.First().Claims;
        
        var roles = claims.Where(claim => claim.ValueType == "role:")
            .Select(claim => claim.Value)
            .ToList().AsReadOnly();
        
        return roles;
    }
}