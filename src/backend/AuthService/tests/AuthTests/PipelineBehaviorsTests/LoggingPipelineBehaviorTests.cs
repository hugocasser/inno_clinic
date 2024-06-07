using Application.PipelineBehaviors;
using Application.Requests.Commands.Login;
using MediatR;
using Microsoft.Extensions.Logging;
using static Moq.It;
using IResult = Application.Abstractions.Results.IResult;

namespace AuthTests.PipelineBehaviorsTests;

[Collection("UnitTest")]
public class LoggingPipelineBehaviorTests : UnitTestFixtures
{
    private readonly Mock<ILogger<LoggingPipelineBehavior<LoginUserCommand, IResult>>> _logger = new();
    [Fact]
    public async Task LoggingPipelineBehavior_ShouldInvokeNextPipeline_WhenDelegateIsValid()
    {
        // Arrange
        var mockedPipeline = new Mock<IPipelineBehavior<LoginUserCommand, IResult>>();
        
        // Act
        var act = await GenerateLoggingPipelineBehaviorBoilerplate(
            new LoginUserCommand("email@mail.com", "password123-R"), 
            () => mockedPipeline.Object.Handle(IsAny<LoginUserCommand>(),
                IsAny<RequestHandlerDelegate<IResult>>(), IsAny<CancellationToken>()));

        // Assert
        mockedPipeline.Verify(pipelineBehavior => pipelineBehavior.Handle(
                IsAny<LoginUserCommand>(), 
                IsAny<RequestHandlerDelegate<IResult>>(), 
                IsAny<CancellationToken>()), 
            Times.Once);
    }
    
    private Task<IResult> GenerateLoggingPipelineBehaviorBoilerplate(
        LoginUserCommand command,
        RequestHandlerDelegate<IResult> next = default)
    {
        var pipeline = new LoggingPipelineBehavior<LoginUserCommand, IResult>(_logger.Object);
        
        return pipeline.Handle(command, next , CancellationToken.None);
    }
}