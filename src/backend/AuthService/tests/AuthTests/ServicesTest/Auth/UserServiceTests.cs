using Microsoft.AspNetCore.Identity;


namespace AuthTests.ServicesTest.Auth;

[Collection("UnitTest")]
public class UserServiceTests : UnitTestFixtures
{
    
    public UserServiceTests()
    {
        UsersService = new UsersService(UserManagerMock.Object, _confirmMessageSenderMock.Object);
    }
    
    [Fact]
    public async Task RegisterUser_ShouldSendEmailConfirmMessageAndReturnSuccess_WhenUserDoesNotExist()
    {
        // Arrange
        UserManagerMock
            .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        UserManagerMock
            .Setup(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        UserManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(null as User);
        
        _confirmMessageSenderMock
            .Setup(sender => sender.SendEmailConfirmMessageAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Verifiable();
        
        // Act
        var result = await UsersService.RegisterUser("email@mail.com", "password123-R", Roles.Patient, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        _confirmMessageSenderMock.Verify(service => service.SendEmailConfirmMessageAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnBadRequest_WhenUserAlreadyExists()
    {
        // Arrange
        UserManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        
        // Act
        var result = await UsersService.RegisterUser("email@mail.com", "password123-R", Roles.Patient, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.UserAlreadyExists);
        result.GetStatusCode().Should().Be(400);
    }

    [Fact]
    public async Task CheckUserPassword_ShouldReturnSuccess_WhenUserExists()
    {
        // Arrange
        UserManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        
        UserManagerMock
            .Setup(manager => manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        
        // Act
        var result = await UsersService.CheckUserPassword("email@mail.com", "password123-R");
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnUnauthorized_WhenRoleIsInvalid()
    {
        // Arrange
        UserManagerMock
            .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        UserManagerMock
            .Setup(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError()
            {
                Code = "code",
                Description = "Invalid role"
            }));
        
        // Act
        var result = await UsersService.RegisterUser("email@mail.com", "password123-R", Roles.Patient, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().NotBeNull();
        result.GetStatusCode().Should().Be(401);
    }
    
    [Fact]
    public async Task CheckUserPassword_ShouldReturnUnauthorized_WhenUserNotFound()
    {
        // Arrange
        UserManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(null as User);
        
        // Act
        var result = await UsersService.CheckUserPassword("email@mail.com", "password123-R");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidEmailOrPassword);
        result.GetStatusCode().Should().Be(401);
    }
    
    [Fact]
    public async Task CheckUserPassword_ShouldReturnUnauthorized_WhenPasswordIsIncorrect()
    {
        // Arrange
        UserManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        
        UserManagerMock
            .Setup(manager => manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(false);
        
        // Act
        var result = await UsersService.CheckUserPassword("email@mail.com", "password123-R");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidEmailOrPassword);
        result.GetStatusCode().Should().Be(401);
    }
}