using Application.Requests.Commands.ConfirmMail;
using FluentAssertions;

namespace AuthTests.ValidationTests;

public class ConfirmMailValidatorTests
{
    [Fact]
    public void ConfirmMailValidatorTest_ShouldPassValidation()
    {
        // Arrange
        var validator = new ConfirmMailCommandValidator();
        
        var command = new ConfirmMailCommand(Guid.NewGuid(), "code");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void ConfirmMailValidatorTest_ShouldFailValidation_WhenCodeIsInvalid()
    {
        // Arrange
        var validator = new ConfirmMailCommandValidator();
        
        var command = new ConfirmMailCommand(Guid.NewGuid(), string.Empty);
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ConfirmMailValidatorTest_ShouldFailValidation_WhenUserIdIsInvalid()
    {
        // Arrange
        var validator = new ConfirmMailCommandValidator();
        
        var command = new ConfirmMailCommand(Guid.Empty, "code");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}