using Domain.Models;

namespace Application.Abstractions.Services;

public interface IRefreshTokensService
{
    public Task<RefreshToken> CreateUserRefreshTokenAsync(User user);
}

