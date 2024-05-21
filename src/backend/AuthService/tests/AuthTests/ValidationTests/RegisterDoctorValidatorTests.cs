using Application.Requests.Commands.RegisterDoctor;
using FluentAssertions;

namespace AuthTests.ValidationTests;

public class RegisterDoctorValidatorTests
{
    [Fact]
    public void RegisterDoctorValidatorTest_ShouldPassValidation()
    {
        // Arrange
        var validator = new RegisterDoctorCommandValidator();
        
        var command = new RegisterDoctorCommand("email@mail.com", "password123-R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Fact]
    public void RegisterDoctorValidatorTest_ShouldFailValidation_WhenPasswordIsInvalid_AndEmailIsInvalid()
    {
        // Arrange
        var validator = new RegisterDoctorCommandValidator();
        
        var command = new RegisterDoctorCommand("", "password123-R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void RegisterDoctorValidatorTest_ShouldFailValidation_WhenPasswordIsInvalid_AndEmailIsValid()
    {
        // Arrange
        var validator = new RegisterDoctorCommandValidator();
        
        var command = new RegisterDoctorCommand("email@mail.com", "password123R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void RegisterDoctorValidatorTest_ShouldFailValidation_WhenPasswordAndEmailAreInvalid()
    {
        // Arrange
        var validator = new RegisterDoctorCommandValidator();
        
        var command = new RegisterDoctorCommand("", "password123R");
        
        // Act
        var result = validator.Validate(command);
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}