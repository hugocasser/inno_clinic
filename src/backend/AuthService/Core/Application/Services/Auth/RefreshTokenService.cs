using Application.Abstractions.Cache;
using Application.Abstractions.Repositories;
using Application.Abstractions.Results;
using Application.Abstractions.Services;
using Application.Common;
using Application.Common.Errors;
using Application.Results;
using Domain.Models;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Application.Services.Auth;

public class RefreshTokenService(ICacheService cacheService,
    IRefreshTokensRepository refreshTokensRepository) : IRefreshTokensService
{
    public async Task<RefreshToken> CreateUserRefreshTokenAsync(User user, CancellationToken cancellationToken)
    {
        var refreshToken = GenerateRefreshToken(user);
        
        await refreshTokensRepository.CreateTokenAsync(refreshToken, cancellationToken);

        await refreshTokensRepository.SaveChangesAsync(cancellationToken);
        await cacheService.SetAsync(refreshToken.UserId.ToString(), JsonConvert.SerializeObject(refreshToken), cancellationToken: cancellationToken);
        
        return refreshToken;
    }
    
    public async Task<IResult> ValidateRefreshTokenAsync(string userId, string refreshToken, CancellationToken cancellationToken)
    {
        var refreshTokenModel = await GetUserRefreshTokenAsync(Guid.Parse(userId), cancellationToken);

        if (refreshTokenModel is null || refreshTokenModel.Token != refreshToken || refreshTokenModel.ExpiryTime < DateTime.Now)
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidRefreshToken));
        }
        
        return ResultWithContent<RefreshToken>.Success(refreshTokenModel);
    }

    private async Task<RefreshToken?> GetUserRefreshTokenAsync(Guid userId, CancellationToken cancellationToken)
    {
        var refreshToken = JsonSerializer.Deserialize<RefreshToken>( await cacheService.GetAsync(userId.ToString(), cancellationToken) ?? string.Empty);

        if (refreshToken is not null)
        {
            return refreshToken;
        }

        refreshToken = await refreshTokensRepository.GetUserRefreshTokenAsync(userId, cancellationToken);

        if (refreshToken is not null)
        {
            await cacheService.SetAsync(refreshToken.UserId.ToString(), JsonConvert.SerializeObject(refreshToken), cancellationToken: cancellationToken);
        }


        return refreshToken;
    }

    private static RefreshToken GenerateRefreshToken(User user)
    {
        return new RefreshToken
        {
            Id = Guid.NewGuid(),
            UserId = user.Id,
            Token = Utilities.GenerateRandomString(7),
            AddedTime = DateTime.UtcNow,
            ExpiryTime = DateTime.UtcNow.AddMonths(2),
            User = user
        };
    }
}