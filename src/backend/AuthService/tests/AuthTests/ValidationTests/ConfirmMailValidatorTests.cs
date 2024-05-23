using Application.Requests.Commands.ConfirmMail;

namespace AuthTests.ValidationTests;

[Collection("UnitTest")]
public class ConfirmMailValidatorTests
{
    private readonly ConfirmMailCommandValidator _validator = new();
    [Theory]
    [InlineData("someCode")]
    [InlineData("someOtherCode")]
    [InlineData("someOtherOtherCode")]
    [InlineData("someOtherOtherOtherCode")]
    public void ConfirmMailValidatorTest_ShouldPassValidation(string code)
    {
        // Act
        var result = _validator.Validate(new ConfirmMailCommand(Guid.NewGuid(), code));
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void ConfirmMailValidatorTest_ShouldFailValidation_WhenCodeIsInvalid(string code)
    {
        // Act
        var result = _validator.Validate(new ConfirmMailCommand(Guid.NewGuid(), code));
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ConfirmMailValidatorTest_ShouldFailValidation_WhenUserIdIsInvalid()
    {
        // Act
        var result = _validator.Validate(new ConfirmMailCommand(Guid.Empty, "someCode"));
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}