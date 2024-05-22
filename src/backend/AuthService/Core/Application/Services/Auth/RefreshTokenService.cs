using Application.Abstractions.Cache;
using Application.Abstractions.Repositories;
using Application.Abstractions.Results;
using Application.Abstractions.Services;
using Application.Common;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
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
        
        var serializedRefreshToken = JsonConvert.SerializeObject(refreshToken, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        });
        await cacheService.SetAsync(refreshToken.UserId.ToString(), serializedRefreshToken , cancellationToken: cancellationToken);
        
        return refreshToken;
    }
    
    public async Task<IResult> ValidateRefreshTokenAsync(string? userId, string refreshToken, CancellationToken cancellationToken)
    {
        var refreshTokenModel = await GetUserRefreshTokenAsync(Guid.Parse(userId ?? string.Empty), cancellationToken);

        if (refreshTokenModel is null || refreshTokenModel.Token != refreshToken)
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidRefreshToken));
        }
        
        return ResultWithContent<RefreshToken>.Success(refreshTokenModel);
    }

    public async Task<IResult> RevokeRefreshTokenAsync(string? userId, CancellationToken cancellationToken)
    {
        var refreshToken = await GetUserRefreshTokenAsync(Guid.Parse(userId ?? string.Empty), cancellationToken);
        if (refreshToken is null)
        {
            return ResultWithoutContent.Success();
        }

        if (refreshToken.UserId.ToString() != userId)
        {
            return ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.AccessDenied));
        }
        
        await refreshTokensRepository.RemoveTokenAsync(refreshToken);
        await refreshTokensRepository.SaveChangesAsync(cancellationToken);
        
        await cacheService.RemoveAsync(refreshToken.UserId.ToString(), cancellationToken);
        
        return ResultWithoutContent.Success();
    }

    private async Task<RefreshToken?> GetUserRefreshTokenAsync(Guid userId, CancellationToken cancellationToken)
    {
        var refreshSerializedToken = await cacheService.GetAsync(userId.ToString(), cancellationToken);
        
        var refreshToken = null as RefreshToken;
        
        if (!string.IsNullOrEmpty(refreshSerializedToken))
        {
            refreshToken = JsonSerializer.Deserialize<RefreshToken>(refreshSerializedToken);
        }
        
        var currentTime = DateTime.Now;
        
        if (refreshToken is not null)
        {
            if (refreshToken.ExpiryTime < currentTime)
            {
                await cacheService.RemoveAsync(refreshToken.UserId.ToString(), cancellationToken);
            }
            else
            {
                return refreshToken;
            }
        }

        refreshToken = await refreshTokensRepository.GetUserRefreshTokenAsync(userId, cancellationToken);

        if (refreshToken is null)
        {
            return null;
        }

        if (refreshToken.ExpiryTime < currentTime)
        {
            await refreshTokensRepository.RemoveTokenAsync(refreshToken);
            
            return null;
        }

        var serializedRefreshToken = JsonConvert.SerializeObject(refreshToken, new JsonSerializerSettings()
        {
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            TypeNameHandling = TypeNameHandling.All
        });
        
        await cacheService.SetAsync(refreshToken.UserId.ToString(), serializedRefreshToken , cancellationToken: cancellationToken);
                
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