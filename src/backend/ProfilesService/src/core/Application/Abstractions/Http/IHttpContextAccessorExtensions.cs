using Application.Common;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Http;

public interface IHttpContextAccessorExtensions : IHttpContextAccessor
{
    public Guid GetUserIdFromClaims();
    public bool CheckUserRoles(EnumRoles role = EnumRoles.Receptionist);
}