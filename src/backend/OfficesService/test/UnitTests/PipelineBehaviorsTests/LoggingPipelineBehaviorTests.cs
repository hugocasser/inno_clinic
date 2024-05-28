using Application.Abstractions.OperationResult;
using Application.PipelineBehaviors;
using Application.Request.Commands.ChangeOfficePhoto;
using MediatR;
using Microsoft.Extensions.Logging;

namespace UnitTests.PipelineBehaviorsTests;

public class LoggingPipelineBehaviorTests
{
    private readonly Mock<ILogger<LoggingPipelineBehavior<ChangeOfficePhotoCommand, IResult>>> _logger = new();
    [Fact]
    public async Task LoggingPipelineBehavior_ShouldInvokeNextPipeline_WhenDelegateIsValid()
    {
        // Arrange
        var mockedPipeline = new Mock<IPipelineBehavior<ChangeOfficePhotoCommand, IResult>>();
        
        // Act
        var act = await GenerateLoggingPipelineBehaviorBoilerplate(
            new ChangeOfficePhotoCommand(Guid.NewGuid(), Guid.NewGuid()), 
            () => mockedPipeline.Object.Handle(It.IsAny<ChangeOfficePhotoCommand>(),
                It.IsAny<RequestHandlerDelegate<IResult>>(), It.IsAny<CancellationToken>()));

        // Assert
        mockedPipeline.Verify(pipelineBehavior => pipelineBehavior.Handle(
                It.IsAny<ChangeOfficePhotoCommand>(), 
                It.IsAny<RequestHandlerDelegate<IResult>>(), 
                It.IsAny<CancellationToken>()), 
            Times.Once);
    }
    
    private Task<IResult> GenerateLoggingPipelineBehaviorBoilerplate(
        ChangeOfficePhotoCommand command,
        RequestHandlerDelegate<IResult> next = default)
    {
        var pipeline = new LoggingPipelineBehavior<ChangeOfficePhotoCommand, IResult>(_logger.Object);
        
        return pipeline.Handle(command, next , CancellationToken.None);
    }
}