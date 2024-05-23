using Application.Requests.Commands.RefreshToken;

namespace AuthTests.ValidationTests;

[Collection("UnitTest")]
public class RefreshTokenValidatorTests
{
    private readonly RefreshTokenCommandValidator _refreshTokenCommandValidator = new();
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
        var result = _refreshTokenCommandValidator.Validate(new RefreshTokenCommand(refreshToken)); 
        
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
        var result = _refreshTokenCommandValidator.Validate(new RefreshTokenCommand(refreshToken)); 
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}