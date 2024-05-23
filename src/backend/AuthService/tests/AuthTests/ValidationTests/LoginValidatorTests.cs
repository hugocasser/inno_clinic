using Application.Requests.Commands.Login;

namespace AuthTests.ValidationTests;

[Collection("UnitTest")]
public class LoginValidatorTests
{
    private readonly LoginUserCommandValidator _validator = new();
    [Theory]
    [InlineData("email@mail.com", "password123-R")]
    [InlineData("email@mail.com", "password?123R")]
    [InlineData("email@mail.com", "password()T123")]
    public void LoginValidatorTests_ShouldPassValidation(string email, string password)
    {
        // Act
        var result = _validator.Validate(new LoginUserCommand(email, password));
        
        // Assert
        result.IsValid.Should().BeTrue();
    }
    
    [Theory]
    [InlineData("", "password123-R")]
    [InlineData(null, "password123-R")]
    [InlineData("  ", "password123-R")]
    [InlineData("email@mail.com", "")]
    [InlineData("email@mail.com", null)]
    [InlineData("email@mail.com", "  ")]
    [InlineData("emailmail.com", "password123")]
    
    public void LoginValidatorTests_ShouldFailValidation_WhenPasswordOrEmailIsInvalid_AndEmailIsInvalid(string email, string password)
    {
        // Act
        var result = _validator.Validate(new LoginUserCommand(email, password));
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}