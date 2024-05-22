using Application.Abstractions.Results;
using Application.PipelineBehaviors;
using Application.Requests.Commands.Login;
using AuthTests.Fixtures;
using FluentAssertions;
using FluentValidation;
using MediatR;
using Moq;

namespace AuthTests.PipelineBehaviorsTests;

[Collection("UnitTest")]
public class ValidationPipelineBehaviorTests : UnitTestFixtures
{
    
    [Fact]
    public async Task ValidationPipelineBehavior_ShouldReturnsResultWithBadRequest_WhenValidationFails()
    {
        // Arrange
        var pipeline = new ValidationPipelineBehavior<LoginUserCommand, IResult>(Validators);
        
        // Act
        var result = await pipeline.Handle(
            new LoginUserCommand("d", "d"), default!, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetStatusCode().Should().Be(400);
    }

    [Fact]
    public async Task ValidationPipelineBehavior_ShouldInvokeNextPipeline_WhenValidationSucceeds()
    {
        // Arrange
        var mockedPipeline = new Mock<IPipelineBehavior<LoginUserCommand, IResult>>();

        // Act
        var act = await GenerateValidationPipelineBehaviorBoilerplate(new LoginUserCommand("email@mail.com", "password123-R"),
            Validators,
            () => mockedPipeline.Object.Handle(It.IsAny<LoginUserCommand>(),
                It.IsAny<RequestHandlerDelegate<IResult>>(), It.IsAny<CancellationToken>()));

        // Assert
        mockedPipeline.Verify(
            x => x.Handle(It.IsAny<LoginUserCommand>(), It.IsAny<RequestHandlerDelegate<IResult>>(),
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