using Application.Request.Commands.ChangeOfficeStatus;
using FluentValidation.TestHelper;

namespace UnitTests.Validators;

public class ChangeOfficeStatusCommandValidatorTests
{
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Validate_ShouldNotHaveValidationError(bool active)
    {
        // Arrange
        var validator = new ChangeOfficeStatusCommandValidator();
        var command = new ChangeOfficeStatusCommand(Guid.NewGuid(), active);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }

    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public void Validate_ShouldHaveValidationError(bool active)
    {
        // Arrange
        var validator = new ChangeOfficeStatusCommandValidator();
        var command = new ChangeOfficeStatusCommand(Guid.Empty, active);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveAnyValidationError();
    }
}