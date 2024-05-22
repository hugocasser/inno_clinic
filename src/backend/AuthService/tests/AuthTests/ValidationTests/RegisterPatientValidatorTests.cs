namespace AuthTests.ValidationTests;

[Collection("UnitTest")]
public class RegisterPatientValidatorTests : UnitTestFixtures
{
    [Theory]
    [InlineData("email@mail.com", "password123-R")]
    [InlineData("email@mail.com", "password?123R")]
    [InlineData("email@mail.com", "password()T123")]
    public void RegisterDoctorValidatorTest_ShouldPassValidation(string email, string password)
    {
        // Act
        var result = RegisterPatientCommandValidator.Validate(CreateRegisterPatientCommand(email, password));
        
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
    public void RegisterDoctorValidatorTest_ShouldFailValidation_WhenPasswordOrEmailIsInvalid(string email, string password)
    {
        // Act
        var result = RegisterPatientCommandValidator.Validate(CreateRegisterPatientCommand(email, password));
        
        // Assert
        result.IsValid.Should().BeFalse();
    }
}