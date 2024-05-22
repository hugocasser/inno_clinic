using Application.Abstractions.Auth;
using Application.OperationResult.Errors;
using Application.Services.Auth;
using Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AuthTests.ServicesTest.Auth;

public class UserServiceTests
{
    private readonly Mock<UserManager<User>> _userManagerMock = new (Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    private readonly Mock<IConfirmMessageSenderService> _confirmMessageSenderMock = new ();
    private readonly IUserService _usersService;
    
    public UserServiceTests()
    {
        _usersService = new UsersService(_userManagerMock.Object, _confirmMessageSenderMock.Object);
    }
    
    [Fact]
    public async Task RegisterUser_ShouldSendEmailConfirmMessageAndReturnSuccess_WhenUserDoesNotExist()
    {
        // Arrange
        _userManagerMock
            .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        _userManagerMock
            .Setup(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(null as User);
        
        _confirmMessageSenderMock
            .Setup(sender => sender.SendEmailConfirmMessageAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()))
            .Verifiable();
        
        // Act
        var result = await _usersService.RegisterUser("email@mail.com", "password123-R", Roles.Patient, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        _confirmMessageSenderMock.Verify(service => service.SendEmailConfirmMessageAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once());
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnBadRequest_WhenUserAlreadyExists()
    {
        // Arrange
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        
        // Act
        var result = await _usersService.RegisterUser("email@mail.com", "password123-R", Roles.Patient, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.UserAlreadyExists);
        result.GetStatusCode().Should().Be(400);
    }

    [Fact]
    public async Task CheckUserPassword_ShouldReturnSuccess_WhenUserExists()
    {
        // Arrange
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        
        _userManagerMock
            .Setup(manager => manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(true);
        
        // Act
        var result = await _usersService.CheckUserPassword("email@mail.com", "password123-R");
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }

    [Fact]
    public async Task RegisterUser_ShouldReturnUnauthorized_WhenRoleIsInvalid()
    {
        // Arrange
        _userManagerMock
            .Setup(manager => manager.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        _userManagerMock
            .Setup(manager => manager.AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError()
            {
                Code = "code",
                Description = "Invalid role"
            }));
        
        // Act
        var result = await _usersService.RegisterUser("email@mail.com", "password123-R", Roles.Patient, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().NotBeNull();
        result.GetStatusCode().Should().Be(401);
    }
    
    [Fact]
    public async Task CheckUserPassword_ShouldReturnUnauthorized_WhenUserNotFound()
    {
        // Arrange
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(null as User);
        
        // Act
        var result = await _usersService.CheckUserPassword("email@mail.com", "password123-R");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidEmailOrPassword);
        result.GetStatusCode().Should().Be(401);
    }
    
    [Fact]
    public async Task CheckUserPassword_ShouldReturnUnauthorized_WhenPasswordIsIncorrect()
    {
        // Arrange
        _userManagerMock
            .Setup(manager => manager.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync(new User());
        
        _userManagerMock
            .Setup(manager => manager.CheckPasswordAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(false);
        
        // Act
        var result = await _usersService.CheckUserPassword("email@mail.com", "password123-R");
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidEmailOrPassword);
        result.GetStatusCode().Should().Be(401);
    }
}