using Application.Requests.Commands.ResendConfirmationMail;
using FluentAssertions;

namespace AuthTests.ValidationTests;

public class ResendConfirmationMailValidatorTests
{
    [Fact]
    public void ResendConfirmationMailValidatorTest_ShouldPassValidation()
    {
        // Arrange
        var validator = new ResendConfirmationMailCommandValidator();
        
        var command = new ResendConfirmationMailCommand("email@mail.com", "password123-R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }

    [Fact]
    public void ResendConfirmationMailValidatorTest_ShouldFailValidation_WhenEmailIsInvalid()
    {
        // Arrange
        var validator = new ResendConfirmationMailCommandValidator();

        var command = new ResendConfirmationMailCommand(string.Empty, "password123-R");

        // Act
        var result = validator.Validate(command);

        // Assert
        result.IsValid.Should().BeFalse();

    }
    
    [Fact]
    public void ResendConfirmationMailValidatorTest_ShouldFailValidation_WhenPasswordIsInvalid()
    {
        // Arrange
        var validator = new ResendConfirmationMailCommandValidator();
        
        var command = new ResendConfirmationMailCommand("email@mail.com", string.Empty);
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ResendConfirmationMailValidatorTest_ShouldFailValidation_WhenEmailAndPasswordAreInvalid()
    {
        // Arrange
        var validator = new ResendConfirmationMailCommandValidator();
        
        var command = new ResendConfirmationMailCommand("", string.Empty);
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}