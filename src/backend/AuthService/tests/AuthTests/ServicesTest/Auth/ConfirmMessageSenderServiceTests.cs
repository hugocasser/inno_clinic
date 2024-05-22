using Application.Dtos;
using Application.Options;


namespace AuthTests.ServicesTest.Auth;

[Collection("UnitTest")]
public class ConfirmMessageSenderServiceTests : UnitTestFixtures

{
    public ConfirmMessageSenderServiceTests()
    {
        var emailSenderOptions = new EmailSenderOptions()
        {
            ConfirmUrl = "http://localhost:8080/confirm/",
        };
        MockEmailOptions
            .Setup(options => options.Value)
            .Returns(emailSenderOptions);
        
        ConfirmMessageSenderService = new ConfirmMessageSenderService(MockEmailSenderService.Object, MockEmailOptions.Object, UserManagerMock.Object);
    }


    [Fact]
    public async Task SendEmailConfirmMessageAsync_ShouldCallSendEmailAsync_WhenUserExists()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        
        UserManagerMock
            .Setup(manager => manager.FindByIdAsync(It.IsAny<string>()))
            .ReturnsAsync(user);

        UserManagerMock
            .Setup(manager => manager.GenerateEmailConfirmationTokenAsync(It.IsAny<User>()))
            .ReturnsAsync("token");
        
        // Act
        await ConfirmMessageSenderService.SendEmailConfirmMessageAsync(user, CancellationToken.None);
        
        // Assert
        MockEmailSenderService.Verify(service => service.SendEmailAsync(It.IsAny<EmailMessage>(), It.IsAny<CancellationToken>()), Times.Once);
    }
}