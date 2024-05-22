using Domain.Models;

namespace Application.Abstractions.Auth;

public interface IAccessTokensService
{
    public string CreateAccessToken(User user, IList<string> userRoles, CancellationToken cancellationToken);
    public Task<IList<string>> GetRolesAsync(User user);
}