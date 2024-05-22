using Application.Requests.Commands.ConfirmMail;
using Microsoft.AspNetCore.Identity;

namespace AuthTests.CommandHandlersTests;

[Collection("UnitTest")]
public class ConfirmMailTests : UnitTestFixtures
{
    
    [Fact]
    public async Task ConfirmMailTest_WhenAllCorrect_ShouldReturnNoContent()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        
        UserManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);

        UserManagerMock.Setup(manager => manager
            .ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);
        
        var confirmMailCommand = new ConfirmMailCommand(user.Id, "code");
        var handler = new ConfirmMailCommandHandler(UserManagerMock.Object);
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
        UserManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(null as User);
        var handler = new ConfirmMailCommandHandler(UserManagerMock.Object);
        
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

        UserManagerMock.Setup(manager => manager.FindByIdAsync(It.IsAny<string>())).ReturnsAsync(user);
        UserManagerMock.Setup(manager => manager.ConfirmEmailAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Failed(new IdentityError
            {
                Code = "code",
                Description = "Invalid code"
            }));
        
        var confirmMailCommand = new ConfirmMailCommand(user.Id, "code");
        var handler = new ConfirmMailCommandHandler(UserManagerMock.Object);
        
        // Act
        var result = await handler.Handle(confirmMailCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidConfirmToken);
        result.GetStatusCode().Should().Be(400);
    }
}