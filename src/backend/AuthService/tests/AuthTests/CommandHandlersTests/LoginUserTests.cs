using Application.Abstractions.Auth;
using Application.Abstractions.Services;
using Application.Dtos.Views;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Application.Requests.Commands.Login;
using Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AuthTests.CommandHandlersTests;

public class LoginUserTests
{
    private readonly Mock<UserManager<User>> _userManagerMock = new (Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    private readonly Mock<IRefreshTokensService> _refreshTokensServiceMock = new();
    private readonly Mock<IAccessTokensService> _accessTokensServiceMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    
    [Fact]
    public async Task LoginUserTest_WhenUserNotFound_ShouldReturnUnauthorized()
    {
        // Arrange
        
        _userServiceMock
            .Setup(service => service.CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword)));
        
        var loginUserCommand = new LoginUserCommand("email@mail.com", "password123-R");
        var handler = new LoginUserCommandHandler(_userManagerMock.Object,
            _refreshTokensServiceMock.Object, _accessTokensServiceMock.Object, _userServiceMock.Object);
        
        // Act
        var result = await handler.Handle(loginUserCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidEmailOrPassword);
        result.GetStatusCode().Should().Be(401);
    }
    [Fact]
    public async Task LoginUserTest_WhenAllCorrect_ShouldReturnOk_AndRefreshTokenWithAccessToken()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        
        _userManagerMock.Setup(manager =>
            manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        
        _refreshTokensServiceMock
            .Setup(service => service
                .CreateUserRefreshTokenAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RefreshToken());
        
        _accessTokensServiceMock
            .Setup(service => service
                .GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(new List<string>()
            {
                "staff",
                "admin"
            });
        
        _accessTokensServiceMock
            .Setup(service => service
                .CreateAccessToken(It.IsAny<User>(), It.IsAny<List<string>>()))
            .Returns("token");
        
        _userServiceMock
            .Setup(service => service
                .CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithContent<User>.Success(user));
        
        var loginUserCommand = new LoginUserCommand("email@mail.com", "password123-R");
        var handler = new LoginUserCommandHandler(_userManagerMock.Object,
            _refreshTokensServiceMock.Object, _accessTokensServiceMock.Object, _userServiceMock.Object);
        
        // Act
        var result = await handler.Handle(loginUserCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.As<ResultWithContent<AuthTokenViewDto>>().ResultData.Should().NotBeNull();
        result.GetStatusCode().Should().Be(200);
    }
    
    [Fact]
    public async Task LoginUserTest_WhenPasswordIsIncorrect_ShouldReturnUnauthorized()
    {
        // Arrange
        _userServiceMock
            .Setup(service => service
                .CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword)));
        
        var loginUserCommand = new LoginUserCommand("email@mail.com", "password123-R");
        var handler = new LoginUserCommandHandler(_userManagerMock.Object,
            _refreshTokensServiceMock.Object, _accessTokensServiceMock.Object, _userServiceMock.Object);
        // Act
        var result = await handler.Handle(loginUserCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidEmailOrPassword);
        result.GetStatusCode().Should().Be(401);
    }

    [Fact]
    public async Task LoginUserTest_WhenUserDoNotHaveRoles_ShouldReturnUnauthorized()
    {
        // Arrange
        _userServiceMock
            .Setup(service => service
                .CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithContent<User>.Success(TestUtils.FakeUser()));
        
        _accessTokensServiceMock
            .Setup(service => service
                .GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(new List<string>());
        
        var loginUserCommand = new LoginUserCommand("email@mail.com", "password123-R");
        var handler = new LoginUserCommandHandler(_userManagerMock.Object,
            _refreshTokensServiceMock.Object, _accessTokensServiceMock.Object, _userServiceMock.Object);
        // Act
        var result = await handler.Handle(loginUserCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.UserNotHaveSuitableRole);
        result.GetStatusCode().Should().Be(401);
    }
}