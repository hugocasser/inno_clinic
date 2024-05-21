using Application.Abstractions.Auth;
using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Application.Requests.Commands.ResendConfirmationMail;
using Domain.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace AuthTests.CommandHandlersTests;

public class ResendConfirmationMailTests
{
    private readonly Mock<UserManager<User>> _userManagerMock = new(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    private readonly Mock<IConfirmMessageSenderService> _confirmMessageSenderMock = new();
    private readonly Mock<IUserService> _userServiceMock = new();
    
    [Fact]
    public async Task ResendConfirmationMailTest_WhenUserNotFound_ShouldReturnUnauthorized()
    {
        // Arrange
        _userServiceMock
            .Setup(service => service.CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword)));
        var resendConfirmationMailCommand = new ResendConfirmationMailCommand("email@mail.com", "password123-R");
        var handler = new ResendConfirmationMailCommandHandler(_userManagerMock.Object,
            _confirmMessageSenderMock.Object, _userServiceMock.Object);
        
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
        
        _userServiceMock
            .Setup(service => service.CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithoutContent.Failure(Error.Unauthorized().WithMessage(ErrorMessages.InvalidEmailOrPassword)));
        
        
        var resendConfirmationMailCommand = new ResendConfirmationMailCommand("email@mail.com", "password123-R");
        var handler = new ResendConfirmationMailCommandHandler(_userManagerMock.Object,
            _confirmMessageSenderMock.Object, _userServiceMock.Object);
        
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
        
        _userServiceMock
            .Setup(service => service.CheckUserPassword(It.IsAny<string>(), It.IsAny<string>()))
            .ReturnsAsync(ResultWithContent<User>.Success(user));
        
        var command = new ResendConfirmationMailCommand(user.Email, "password123-R");

        var handler =
            new ResendConfirmationMailCommandHandler(_userManagerMock.Object,
                _confirmMessageSenderMock.Object, _userServiceMock.Object); 

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be(ErrorMessages.EmailAlreadyConfirmed);
        result.GetStatusCode().Should().Be(400);
    }
}