using Application.Requests.Commands.RefreshToken;
using FluentAssertions;

namespace AuthTests.ValidationTests;

public class RefreshTokenValidatorTests
{
    [Fact]
    public void RefreshTokenValidatorTest_ShouldPassValidation()
    {
        // Arrange
        var validator = new RefreshTokenCommandValidator();
        
        var command = new RefreshTokenCommand("refreshToken");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void RefreshTokenValidatorTest_ShouldFailValidation_WhenRefreshTokenIsInvalid()
    {
        // Arrange
        var validator = new RefreshTokenCommandValidator();
        
        var command = new RefreshTokenCommand(string.Empty);
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}