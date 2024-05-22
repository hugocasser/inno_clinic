using Newtonsoft.Json;

namespace AuthTests.ServicesTest.Auth;

[Collection("UnitTest")]
public class RefreshTokenServiceTests : UnitTestFixtures
{
    public RefreshTokenServiceTests()
    {
         RefreshTokenService = new RefreshTokenService(CacheService.Object, RefreshTokensRepository.Object);
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
        var refreshToken = await RefreshTokenService.CreateUserRefreshTokenAsync(user, CancellationToken.None);

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

        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));

        // Act
        var result =
            await RefreshTokenService.ValidateRefreshTokenAsync(user.Id.ToString(), refreshToken,
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
        
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken + "1",
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));
        
        // Act
        var result =
            await RefreshTokenService.ValidateRefreshTokenAsync(user.Id.ToString(), refreshToken,
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
        
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = refreshToken,
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));
        
        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);
        
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
        
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);
        
        RefreshTokensRepository
            .Setup(x => x.GetUserRefreshTokenAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as RefreshToken);
        
        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);
        
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

        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = "refreshToken",
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));

        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);
        
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

        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = user.Id,
                Token = "refreshToken",
                ExpiryTime = DateTime.UtcNow
            }));

        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task RevokeRefreshToken_ShouldReturnUnauthorized_WhenTokenNotBelongsUser()
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
        
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(refreshToken));
        
        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(user.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be(401);
    }
}