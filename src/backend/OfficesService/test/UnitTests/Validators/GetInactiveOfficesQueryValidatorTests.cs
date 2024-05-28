using Application.Request.Queries.GetInactiveOffices;
using FluentValidation.TestHelper;

namespace UnitTests.Validators;

public class GetInactiveOfficesQueryValidatorTests
{
    [Fact]
    public void Validate_ShouldNotHaveValidationError()
    {
        // Arrange
        var validator = new GetInactiveOfficesQueryValidator();
        var query = new GetInactiveOfficesQuery(Utilities.GenerateNullPageSettings());
        
        // Act
        var result = validator.TestValidate(query);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public void Validate_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new GetInactiveOfficesQueryValidator();
        var query = new GetInactiveOfficesQuery(Utilities.GenerateNotValidPageSettings());
        
        // Act
        var result = validator.TestValidate(query);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSettings);
    }
}