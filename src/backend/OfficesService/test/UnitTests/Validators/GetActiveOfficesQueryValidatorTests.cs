using Application.Request.Queries.GetActiveOffices;
using FluentValidation.TestHelper;

namespace UnitTests.Validators;

public class GetActiveOfficesQueryValidatorTests
{
    [Fact]
    public void Validate_ShouldNotHaveValidationError()
    {
        // Arrange
        var validator = new GetActiveOfficesQueryValidator();
        var query = new GetActiveOfficesQuery(Utilities.GenerateNullPageSettings());
        
        // Act
        var result = validator.TestValidate(query);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Fact]
    public void Validate_ShouldHaveValidationError()
    {
        // Arrange
        var validator = new GetActiveOfficesQueryValidator();
        var query = new GetActiveOfficesQuery(Utilities.GenerateNotValidPageSettings());
        
        // Act
        var result = validator.TestValidate(query);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSettings);
    }
}