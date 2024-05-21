using Application.Requests.Commands.RegisterPatient;
using FluentAssertions;

namespace AuthTests.ValidationTests;

public class RegisterPatientValidatorTests
{
    [Fact]
    public void RegisterPatientValidatorTest_ShouldPassValidation()
    {
        // Arrange
        var validator = new RegisterPatientCommandValidator();
        
        var command = new RegisterPatientCommand("email@mail.com", "password123-R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void RegisterPatientValidatorTest_ShouldFailValidation_WhenPasswordIsInvalid_AndEmailIsInvalid()
    {
        // Arrange
        var validator = new RegisterPatientCommandValidator();
        
        var command = new RegisterPatientCommand("", "password123-R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void RegisterPatientValidatorTest_ShouldFailValidation_WhenPasswordIsInvalid_AndEmailIsValid()
    {
        // Arrange
        var validator = new RegisterPatientCommandValidator();
        
        var command = new RegisterPatientCommand("email@mail.com", "password123R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void RegisterPatientValidatorTest_ShouldFailValidation_WhenPasswordAndEmailAreInvalid()
    {
        // Arrange
        var validator = new RegisterPatientCommandValidator();
        
        var command = new RegisterPatientCommand("", "password123R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}