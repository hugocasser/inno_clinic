using Application.OperationResult.Errors;
using Application.OperationResult.Results;
using Domain.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace AuthTests.OperationResultTests;

public class ResultWithContentTests
{
    [Fact]
    public void GetResultMessage_ShouldReturnSerializedContentWith200Code_WhenResultIsSuccess()
    {
        // Arrange
        var user = TestUtils.FakeUser();
        
        // Act
        var result = ResultWithContent<User>.Success(user);
        var objectResult = JsonConvert.DeserializeObject<User>(result.GetResultMessage());
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.GetStatusCode().Should().Be(200);
        objectResult.Should().BeEquivalentTo(user, options => options.ExcludingMissingMembers());
    }
    
    [Fact]
    public void GetResultMessage_ShouldReturnSerializedContentWith400Code_WhenResultIsFailure()
    {
        // Arrange
        var error = Error.BadRequest().WithMessage("Test message");
        
        // Act
        var result = ResultWithContent<User>.Failure(error);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.GetStatusCode().Should().Be(400);
        result.GetResultMessage().Should().Be("Test message");
    }
}