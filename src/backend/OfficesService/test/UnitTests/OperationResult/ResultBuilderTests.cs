namespace UnitTests.OperationResult;

public class ResultBuilderTests
{
    [Fact]
    public void Success_ShouldReturnSuccess()
    {
        // Act
        var result = ResultBuilder.Success();
        
        // Assert
        result.GetOperationResult().Should().Be("No content");
        result.IsSuccess.Should().BeTrue();
        result.GetStatusCode().Should().Be(204);
    }
    
    [Fact]
    public void Failure_ShouldReturnFailure()
    {
        // Act
        var result = ResultBuilder.Failure();
        
        // Assert
        result.GetOperationResult().Should().Be("Unprocessed error");
        result.IsSuccess.Should().BeFalse();
        result.GetStatusCode().Should().Be(500);
    }
    
    [Fact]
    public void WithData_ShouldReturnWithData()
    {
        // Act
        var result = ResultBuilder.Success().WithData("some data");
        
        // Assert
        result.GetOperationResult().Should().Be("some data");
        result.IsSuccess.Should().BeTrue();
        result.GetStatusCode().Should().Be(204);
    }
    
    [Fact]
    public void WithStatusCode_ShouldReturnWithStatusCode()
    {
        // Act
        var result = ResultBuilder.Success().WithStatusCode(201);
        
        // Assert
        result.GetOperationResult().Should().Be("No content");
        result.IsSuccess.Should().BeTrue();
        result.GetStatusCode().Should().Be(201);
    }
}