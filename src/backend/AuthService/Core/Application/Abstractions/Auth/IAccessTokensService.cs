using Domain.Models;

namespace Application.Abstractions.Auth;

public interface IAccessTokensService
{
    public string CreateAccessToken(User user, IList<string> userRoles);
    public Task<IList<string>> GetRolesAsync(User user);
}