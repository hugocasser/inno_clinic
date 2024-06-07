using Application.Request.Commands.ChangeOfficePhoto;
using FluentValidation.TestHelper;

namespace UnitTests.Validators;

public class ChangeOfficePhotoCommandValidatorTests
{
    [Fact]
    public void Validate_ShouldNotHaveValidationError()
    {
        // Arrange
        var validator = new ChangeOfficePhotoCommandValidator();
        var command = new ChangeOfficePhotoCommand(Guid.NewGuid(), Guid.NewGuid());
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public void Validate_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new ChangeOfficePhotoCommandValidator();
        var command = new ChangeOfficePhotoCommand(Guid.Empty, Guid.Empty);
        
        // Act
        var result = validator.TestValidate(command);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.OfficeId);
        result.ShouldHaveValidationErrorFor(x => x.PhotoId);
    }
}