using Application.Abstractions.Auth;
using Application.Abstractions.Email;
using Application.Dtos;
using Application.Options;
using Application.Services.Auth;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;

namespace AuthTests.ServicesTest.Auth;

public class ConfirmMessageSenderServiceTests
{
    private readonly Mock<IEmailSenderService> _mockEmailSenderService = new();
    private readonly Mock<IOptions<EmailSenderOptions>> _mockOptions = new();
    private readonly Mock<UserManager<User>> _mockUserManager = new(Mock.Of<IUserStore<User>>(), null, null, null, null, null, null, null, null);
    private readonly IConfirmMessageSenderService _confirmMessageSenderService;
    public ConfirmMessageSenderServiceTests()
    {
        var emailSenderOptions = new EmailSenderOptions()
        {
            ConfirmUrl = "http://localhost:8080/confirm/",
        };
        _mockOptions
            .Setup(options => options.Value)
            .Returns(emailSenderOptions);
        
        _confirmMessageSenderService = new ConfirmMessageSenderService(_mockEmailSenderService.Object, _mockOptions.Object, _mockUserManager.Object);
    }


    [Fact]
    public async Task SendEmailConfirmMessageAsync_ShouldCallSendEmailAsync_WhenUserExists()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        
        _mockUserManager
            .Setup(manager => manager.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        _mockUserManager
            .Setup(manager => manager.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
            .ReturnsAsync("token");
        
        // Act
        await _confirmMessageSenderService.SendEmailConfirmMessageAsync(user, CancellationToken.None);
        
        // Assert
        _mockEmailSenderService.Verify(service => service.SendEmailAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}