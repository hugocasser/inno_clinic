namespace AuthTests.ValidationTests;

[Collection("UnitTest")]
public class ConfirmMailValidatorTests : UnitTestFixtures
{
    [Theory]
    [InlineData("someCode")]
    [InlineData("someOtherCode")]
    [InlineData("someOtherOtherCode")]
    [InlineData("someOtherOtherOtherCode")]
    public void ConfirmMailValidatorTest_ShouldPassValidation(string code)
    {
        // Act
        var result = ConfirmMailCommandValidator.Validate(CreateConfirmMailCommand(Guid.NewGuid(), code));
        
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
        var result = ConfirmMailCommandValidator.Validate(CreateConfirmMailCommand(Guid.NewGuid(), code));
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
    
    [Fact]
    public void ConfirmMailValidatorTest_ShouldFailValidation_WhenUserIdIsInvalid()
    {
        // Act
        var result = ConfirmMailCommandValidator.Validate(CreateConfirmMailCommand(Guid.Empty, "someCode"));
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}