using Application.Abstractions.Auth;
using Application.Abstractions.Services;
using Application.Options;
using Application.Services.Auth;
using Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace AuthTests.ServicesTest.Auth;

public class AccessTokensServiceTests
{
    private readonly Mock<IOptions<AccessTokenOptions>> _mockOptions = new();
    private readonly Mock<IRefreshTokensService> _mockRefreshTokensService = new();
    private readonly Mock<UserManager<User>> _userManagerMock = new (Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    private readonly IAccessTokensService _accessTokensService;
    
    public AccessTokensServiceTests()
    {
        _mockOptions
            .Setup(options => options.Value)
            .Returns(new AccessTokenOptions
            {
                Audience = "audience",
                Issuer = "issuer",
                Key = "0567e065-e6a5-4c25-aa36-e8304303b14b"

            });
        _accessTokensService = new AccessTokensService(_mockOptions.Object, _userManagerMock.Object, _mockRefreshTokensService.Object);
    }
    

    [Fact]
    public  void CreateAccessToken_ShouldBeCreated_WhenRolesNotNull()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        var roles = new List<string>() {"role1", "role2"};
        
        _userManagerMock
            .Setup(manager => manager.GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(roles);
        
        // Act
        var result =  _accessTokensService.CreateAccessToken(user, roles, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public void CreateAccessToken_ShouldReturnEmpty_WhenRolesNull()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        
        // Act
        var result =  _accessTokensService.CreateAccessToken(user, null, CancellationToken.None);
        
        // Assert
        result.Should().BeNullOrEmpty();
    }
}