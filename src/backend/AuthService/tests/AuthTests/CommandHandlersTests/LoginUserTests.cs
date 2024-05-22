using Application.Dtos.Views;
using Application.Requests.Commands.Login;

namespace AuthTests.CommandHandlersTests;

[Collection("UnitTest")]
public class LoginUserTests : UnitTestFixtures
{
    [Fact]
    public async Task LoginUserTest_WhenUserNotFound_ShouldReturnUnauthorized()
    {
        // Arrange
        
        UserServiceMock
            .Setup(service => service.CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword)));
        
        var loginUserCommand = new LoginUserCommand("email@mail.com", "password123-R");
        var handler = new LoginUserCommandHandler(UserManagerMock.Object,
            RefreshTokensServiceMock.Object, AccessTokensServiceMock.Object, UserServiceMock.Object);
        
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
        
        UserManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(user);
        
        UserManagerMock.Setup(manager =>
            manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        
        RefreshTokensServiceMock
            .Setup(service => service
                .CreateUserRefreshTokenAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new RefreshToken());
        
        AccessTokensServiceMock
            .Setup(service => service
                .GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(new List<string>()
            {
                "staff",
                "admin"
            });
        
        AccessTokensServiceMock
            .Setup(service => service
                .CreateAccessToken(It.IsAny<User>(), It.IsAny<List<string>>()))
            .Returns("token");
        
        UserServiceMock
            .Setup(service => service
                .CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithContent<User>.Success(user));
        
        var loginUserCommand = new LoginUserCommand("email@mail.com", "password123-R");
        var handler = new LoginUserCommandHandler(UserManagerMock.Object,
            RefreshTokensServiceMock.Object, AccessTokensServiceMock.Object, UserServiceMock.Object);
        
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
        UserServiceMock
            .Setup(service => service
                .CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword)));
        
        var loginUserCommand = new LoginUserCommand("email@mail.com", "password123-R");
        var handler = new LoginUserCommandHandler(UserManagerMock.Object,
            RefreshTokensServiceMock.Object, AccessTokensServiceMock.Object, UserServiceMock.Object);
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
        UserServiceMock
            .Setup(service => service
                .CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithContent<User>.Success(TestUtils.FakeUser()));
        
        AccessTokensServiceMock
            .Setup(service => service
                .GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(new List<string>());
        
        var loginUserCommand = new LoginUserCommand("email@mail.com", "password123-R");
        var handler = new LoginUserCommandHandler(UserManagerMock.Object,
            RefreshTokensServiceMock.Object, AccessTokensServiceMock.Object, UserServiceMock.Object);
        // Act
        var result = await handler.Handle(loginUserCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.UserNotHaveSuitableRole);
        result.GetStatusCode().Should().Be(401);
    }
}