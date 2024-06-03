using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Http;

public interface IHttpContextAccessorExtensions : IHttpContextAccessor
{
    public Guid GetUserIdFromClaims();
    public IReadOnlyCollection<string> GetUserRoleFromClaims();
}