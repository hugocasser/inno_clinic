using Domain.Models;

namespace Application.Abstractions.Auth;

public interface IAccessTokensService
{
    public Task<string> CreateAccessToken(User user);
    public Task<string> UpdateAccessToken(RefreshToken refreshToken);
}