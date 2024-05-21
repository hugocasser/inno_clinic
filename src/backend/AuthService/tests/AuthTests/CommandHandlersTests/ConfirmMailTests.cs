using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Application.Requests.Commands.ConfirmMail;
using Bogus;
using Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace AuthTests.CommandHandlersTests;

public class ConfirmMailTests
{
    private readonly Mock<UserManager<User>> _userManagerMock =  new (Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    
    [Fact]
    public async Task ConfirmMailTest_WhenAllCorrect_ShouldReturnNoContent()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        
        _userManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

        _userManagerMock.Setup(manager => manager
            .ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        var confirmMailCommand = new ConfirmMailCommand(user.Id, "code");
        var handler = new ConfirmMailCommandHandler(_userManagerMock.Object);
        // Act
        var result = await handler.Handle(confirmMailCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.GetResultMessage().Should().Be("No content");
    }

    [Fact]
    public async Task ConfirmMailTest_WhenUserNotFound_ShouldReturnNotFound()
    {
        // Arrange
        var confirmMailCommand = new ConfirmMailCommand(Guid.NewGuid(), "code");
        _userManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(null as User);
        var handler = new ConfirmMailCommandHandler(_userManagerMock.Object);
        
        // Act
        var result = await handler.Handle(confirmMailCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.UserNotFound);
        result.GetStatusCode().Should().Be(404);
    }

    [Fact]
    public async Task ConfirmMailTest_WhenCodeIsInvalid_ShouldReturnBadRequest()
    {
        // Arrange
        var user = TestUtils.FakeUser();

        _userManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        _userManagerMock.Setup(manager => manager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError
            {
                Code = "code",
                Description = "Invalid code"
            }));
        
        var confirmMailCommand = new ConfirmMailCommand(user.Id, "code");
        var handler = new ConfirmMailCommandHandler(_userManagerMock.Object);
        
        // Act
        var result = await handler.Handle(confirmMailCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidConfirmToken);
        result.GetStatusCode().Should().Be(400);
    }
}