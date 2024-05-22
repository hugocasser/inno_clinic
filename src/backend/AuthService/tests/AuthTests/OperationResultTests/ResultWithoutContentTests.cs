using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using FluentAssertions;

namespace AuthTests.OperationResultTests;

public class ResultWithoutContentTests
{
    [Fact]
    public void IsSuccess_ShouldReturnTrueWithoutContentAnd204Code_WhenResultIsSuccess()
    {
        // Act
        var result = ResultWithoutContent.Success();
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.GetResultMessage().Should().Be("No content");
        result.GetStatusCode().Should().Be(204);
    }
    
    [Fact]
    public void IsSuccess_ShouldReturnFalseWithoutContentAnd401Code_WhenResultIsFailure()
    {
        // Act
        var result = ResultWithoutContent.Failure(Error.Unauthorized().WithMessage("Test message"));
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetResultMessage().Should().Be("Test message");
        result.GetStatusCode().Should().Be(401);
    }
}