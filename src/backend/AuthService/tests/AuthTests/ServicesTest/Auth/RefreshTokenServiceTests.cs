using Newtonsoft.Json;

namespace AuthTests.ServicesTest.Auth;

[Collection("UnitTest")]
public class RefreshTokenServiceTests : UnitTestFixtures
{
    private const string CacheKey = "RefreshToken";

    private User User { get; set; } = new User()
    {
        Id = Guid.NewGuid()
    };
    public RefreshTokenServiceTests()
    {
         RefreshTokenService = new RefreshTokenService(CacheService.Object, RefreshTokensRepository.Object);
    }

    [Fact]
    public async Task CreateUserRefreshTokenAsync_ShouldCreateRefreshToken()
    {
        // Act
        var refreshToken = await RefreshTokenService.CreateUserRefreshTokenAsync(User, CancellationToken.None);

        // Assert
        refreshToken.Should().NotBeNull();
    }

    [Fact]
    public async Task ValidateRefreshTokenAsync_ShouldPassValidation()
    {
        // Arrange
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = User.Id,
                Token = CacheKey,
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));

        // Act
        var result =
            await RefreshTokenService.ValidateRefreshTokenAsync(User.Id.ToString(), CacheKey,
                CancellationToken.None);
    }

    [Fact]
    public async Task ValidateRefreshTokenAsync_ShouldReturnUnauthorized_WhenTokensNotEqual()
    {
        // Arrange
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = User.Id,
                Token = CacheKey + "1",
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));
        
        // Act
        var result =
            await RefreshTokenService.ValidateRefreshTokenAsync(User.Id.ToString(), CacheKey,
                CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be(401);
        
    }

    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldRevokeRefreshToken()
    {
        // Arrange
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = User.Id,
                Token = CacheKey,
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));
        
        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(User.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldReturnSuccess_WhenRefreshTokenNotFound()
    {
        // Arrange
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(string.Empty);
        
        RefreshTokensRepository
            .Setup(x => x.GetUserRefreshTokenAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as RefreshToken);
        
        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(User.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldReturnSuccess_WhenRefreshTokenNotExpired()
    {
        // Arrange
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = User.Id,
                Token = CacheKey,
                ExpiryTime = DateTime.UtcNow.AddMonths(2)
            }));

        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(User.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task RevokeRefreshTokenAsync_ShouldReturnSuccess_WhenRefreshTokenExpired()
    {
        // Arrange
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(new RefreshToken
            {
                UserId = User.Id,
                Token = CacheKey,
                ExpiryTime = DateTime.UtcNow
            }));

        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(User.Id.ToString(), CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task RevokeRefreshToken_ShouldReturnUnauthorized_WhenTokenNotBelongsUser()
    {
        // Arrange
        var refreshToken = new RefreshToken()
        {
            Id = Guid.NewGuid(),
            UserId = Guid.NewGuid(),
            Token = CacheKey,
            AddedTime = DateTime.UtcNow,
            ExpiryTime = DateTime.UtcNow.AddMonths(2)
        };
        
        CacheService
            .Setup(x => x.GetAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(JsonConvert.SerializeObject(refreshToken));
        
        // Act
        var result = await RefreshTokenService.RevokeRefreshTokenAsync(User.Id.ToString(), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Code.Should().Be(401);
    }
}