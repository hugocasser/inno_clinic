using Application.Abstractions.Results;
using Domain.Models;

namespace Application.Abstractions.Services;

public interface IRefreshTokensService
{
    public Task<RefreshToken> CreateUserRefreshTokenAsync(User user, CancellationToken cancellationToken);
    public Task<IResult> ValidateRefreshTokenAsync(string userId, string refreshToken, CancellationToken cancellationToken);
}

