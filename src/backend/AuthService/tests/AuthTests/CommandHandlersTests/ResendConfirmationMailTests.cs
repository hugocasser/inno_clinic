using Application.Requests.Commands.ResendConfirmationMail;

namespace AuthTests.CommandHandlersTests;

[Collection("UnitTest")]
public class ResendConfirmationMailTests  : UnitTestFixtures
{
    private const string Email = "email@mail.com";
    private const string Password = "password123-R";
    [Fact]
    public async Task ResendConfirmationMailTest_WhenUserNotFound_ShouldReturnUnauthorized()
    {
        // Arrange
        UserServiceMock
            .Setup(service => service.CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword)));
        var resendConfirmationMailCommand = new ResendConfirmationMailCommand(Email, Password);
        var handler = new ResendConfirmationMailCommandHandler(UserManagerMock.Object,
            _confirmMessageSenderMock.Object, UserServiceMock.Object);
        
        // Act
        var result = await handler.Handle(resendConfirmationMailCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidEmailOrPassword);
        result.GetStatusCode().Should().Be(401);
    }

    [Fact]
    public async Task ResendConfirmationMailTest_WhenPasswordIsWrong_ShouldReturnUnauthorized()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        
        UserServiceMock
            .Setup(service => service.CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword)));
        
        
        var resendConfirmationMailCommand = new ResendConfirmationMailCommand(Email, Password);
        var handler = new ResendConfirmationMailCommandHandler(UserManagerMock.Object,
            _confirmMessageSenderMock.Object, UserServiceMock.Object);
        
        // Act
        var result = await handler.Handle(resendConfirmationMailCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.InvalidEmailOrPassword);
        result.GetStatusCode().Should().Be(401);
    }
    [Fact]
    public async Task ResendConfirmationMailTest_WhenUserAlreadyConfirmed_ShouldReturnBadRequest()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        user.EmailConfirmed = true;
        
        UserServiceMock
            .Setup(service => service.CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithContent<User>.Success(user));
        
        var command = new ResendConfirmationMailCommand(user.Email, Password);

        var handler =
            new ResendConfirmationMailCommandHandler(UserManagerMock.Object,
                _confirmMessageSenderMock.Object, UserServiceMock.Object); 

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.EmailAlreadyConfirmed);
        result.GetStatusCode().Should().Be(400);
    }
}