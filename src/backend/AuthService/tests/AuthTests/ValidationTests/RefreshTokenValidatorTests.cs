namespace AuthTests.ValidationTests;

[Collection("UnitTest")]
public class RefreshTokenValidatorTests : UnitTestFixtures
{
    [Theory]
    [InlineData("refreshToken")]
    [InlineData("refreshToken123")]
    [InlineData("refreshToken()")]
    [InlineData("refreshToken?")]
    [InlineData("refreshToken-")]
    [InlineData("refreshToken_")]
    public void RefreshTokenValidatorTest_ShouldPassValidation(string refreshToken)
    {
        // Act
        var result = RefreshTokenCommandValidator.Validate(CreateRefreshTokenCommand(refreshToken));
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    [InlineData("  ")]
    public void RefreshTokenValidatorTest_ShouldFailValidation_WhenRefreshTokenIsInvalid(string refreshToken)
    {
        // Act
        var result = RefreshTokenCommandValidator.Validate(CreateRefreshTokenCommand(refreshToken));
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}