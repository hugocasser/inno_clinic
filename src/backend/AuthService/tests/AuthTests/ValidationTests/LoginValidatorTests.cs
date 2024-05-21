using Application.Requests.Commands.Login;
using Bogus;
using FluentAssertions;

namespace AuthTests.ValidationTests;

public class LoginValidatorTests
{
    [Fact]
    public void LoginValidatorTests_ShouldPassValidation()
    {
        // Arrange
        var validator = new LoginUserCommandValidator();

        var command = new LoginUserCommand("email@mail.com", "password123-R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void LoginValidatorTests_ShouldFailValidation_WhenPasswordIsInvalid_AndEmailIsInvalid()
    {
        // Arrange
        var validator = new LoginUserCommandValidator();
        
        var command = new LoginUserCommand("", "password123-R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void LoginValidatorTests_ShouldFailValidation_WhenPasswordIsInvalid_AndEmailIsValid()
    {
        // Arrange
        var validator = new LoginUserCommandValidator();
        
        var command = new LoginUserCommand("email@mail.com", "password123R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void LoginValidatorTests_ShouldFailValidation_WhenPasswordAndEmailAreInvalid()
    {
        // Arrange
        var validator = new LoginUserCommandValidator();
        
        var command = new LoginUserCommand("", "password123R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}