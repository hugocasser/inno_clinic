using Application.Abstractions.OperationResult;
using Application.PipelineBehaviors;
using Application.Request.Commands.ChangeOfficePhoto;
using FluentValidation;
using MediatR;

namespace UnitTests.PipelineBehaviorsTests;

public class ValidationPipelineBehaviorTests
{
    private readonly IEnumerable<IValidator<ChangeOfficePhotoCommand>> _validators = new List<IValidator<ChangeOfficePhotoCommand>>() 
    { 
        new ChangeOfficePhotoCommandValidator()
    };
    
    [Fact]
    public async Task ValidationPipelineBehavior_ShouldReturnsResultWithBadRequest_WhenValidationFails()
    {
        // Arrange
        var pipeline = new ValidationPipelineBehavior<ChangeOfficePhotoCommand, IResult>(_validators);
        
        // Act
        var result = await pipeline.Handle(
            new ChangeOfficePhotoCommand(Guid.Empty, Guid.Empty), default!, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetStatusCode().Should().Be(400);
    }

    [Fact]
    public async Task ValidationPipelineBehavior_ShouldInvokeNextPipeline_WhenValidationSucceeds()
    {
        // Arrange
        var mockedPipeline = new Mock<IPipelineBehavior<ChangeOfficePhotoCommand, IResult>>();

        // Act
        var act = await GenerateValidationPipelineBehaviorBoilerplate(new ChangeOfficePhotoCommand(Guid.NewGuid(), Guid.NewGuid()),
            _validators,
            () => mockedPipeline.Object.Handle(It.IsAny<ChangeOfficePhotoCommand>(),
                It.IsAny<RequestHandlerDelegate<IResult>>(), It.IsAny<CancellationToken>()));

        // Assert
        mockedPipeline.Verify(
            x => x.Handle(It.IsAny<ChangeOfficePhotoCommand>(), It.IsAny<RequestHandlerDelegate<IResult>>(),
                It.IsAny<CancellationToken>()), Times.Once);
    }

    private Task<TResponse> GenerateValidationPipelineBehaviorBoilerplate<TRequest, TResponse>(
        TRequest command,
        IEnumerable<IValidator<TRequest>> validators,
        RequestHandlerDelegate<TResponse> next = default) where TRequest : IRequest<TResponse>, IRequest<IResult>
        where TResponse : IResult
    {
        var pipeline = new ValidationPipelineBehavior<TRequest, TResponse>(validators);
        
        return pipeline.Handle(command, next , CancellationToken.None);
    }
}