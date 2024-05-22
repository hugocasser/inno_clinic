using Application.Abstractions.Cache;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Services.Auth;
using Domain.Models;
using FluentAssertions;
using Moq;
using Newtonsoft.Json;

namespace AuthTests.ServicesTest.Auth;

public class RefreshTokenServiceTests
{
    private readonly Mock<ICacheService> _cacheService = new();
    private readonly Mock<IRefreshTokensRepository> _refreshTokensRepository = new();
    private readonly IRefreshTokensService _refreshTokenService;
    
    public RefreshTokenServiceTests()
    {
         _refreshTokenService = new RefreshTokenService(_cacheService.Object, _refreshTokensRepository.Object);
    }

    [Fact]
    public async Task CreateUserRefreshTokenAsync_ShouldCreateRefreshToken()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
        };

        // Act
        var refreshToken = await _refreshTokenService.CreateUserRefreshTokenAsync(user, CancellationToken.None);

        // Assert
        refreshToken.Should().NotBeNull();
    }

    [Fact]
    public async Task ValidateRefreshTokenAsync_ShouldPassValidation()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
        };

        var refreshToken = "refreshToken";

        _cacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));

        // Act
        var result =
            await _refreshTokenService.ValidateRefreshTokenAsync(user.Id.ToString(), refreshToken,
                CancellationToken.None);
    }

    [Fact]
    public async Task ValidateRefreshTokenAsync_ShouldReturnUnauthorized_WhenTokensNotEqual()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
        };
        
        var refreshToken = "refreshToken";
        
        _cacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken + "1",
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));
        
        // Act
        var result =
            await _refreshTokenService.ValidateRefreshTokenAsync(user.Id.ToString(), refreshToken,
                CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be(401);
        
    }

    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldRevokeRefreshToken()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
        };
        var refreshToken = "refreshToken";
        
        _cacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));
        
        // Act
        var result = await _refreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldReturnSuccess_WhenRefreshTokenNotFound()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
        };
        
        _cacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);
        
        _refreshTokensRepository
            .Setup(x => x.GetUserRefreshTokenAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as RefreshToken);
        
        // Act
        var result = await _refreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldReturnSuccess_WhenRefreshTokenNotExpired()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
        };

        _cacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = "refreshToken",
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));

        // Act
        var result = await _refreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldReturnSuccess_WhenRefreshTokenExpired()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
        };

        _cacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = "refreshToken",
                ExpiryTime = DateTime.UtcNow
            }));

        // Act
        var result = await _refreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task RevokeRefreshToken_ShouldReturnForbidden_WhenTokenNotBelongsUser()
    {
        // Arrange
        var user = new User
        {
            Id = Guid.NewGuid(),
        };
        
        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Token = "refreshToken",
            AddedTime = DateTime.UtcNow,
            ExpiryTime = DateTime.UtcNow.AddMonths(2)
        };
        
        _cacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(refreshToken));
        
        // Act
        var result = await _refreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be(403);
    }
}