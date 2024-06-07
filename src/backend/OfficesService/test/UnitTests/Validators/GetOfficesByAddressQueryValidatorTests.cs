using Application.Request.Queries.GetOfficesByAddress;
using FluentValidation.TestHelper;

namespace UnitTests.Validators;

public class GetOfficesByAddressQueryValidatorTests
{
    
    [Theory]
    [InlineData("121423423423",false)]
    [InlineData("121423423423",true)]
    public void Validate_ShouldNotHaveValidationError(string number, bool active)
    {
        // Arrange
        var validator = new GetOfficesByAddressQueryValidator();
        var query = new GetOfficesByAddressQuery(number, Utilities.GenerateValidPageSettings(), active);
        
        // Act
        var result = validator.TestValidate(query);
        
        // Assert
        result.ShouldNotHaveAnyValidationErrors();
    }
    
    [Theory]
    [InlineData(null ,false)]
    [InlineData("",true)]
    public void Validate_ShouldHaveValidationError(string? number, bool active)
    {
        // Arrange
        var validator = new GetOfficesByAddressQueryValidator();
        var query = new GetOfficesByAddressQuery(number, Utilities.GenerateNotValidPageSettings(), active);
        
        // Act
        var result = validator.TestValidate(query);
        
        // Assert
        result.ShouldHaveValidationErrorFor(x => x.PageSettings);
    }
}