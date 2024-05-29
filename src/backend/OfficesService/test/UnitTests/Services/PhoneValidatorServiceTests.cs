using System.Net;
using Application.Dtos.Validation.PhoneValidation;

namespace UnitTests.Services;

public class PhoneValidatorServiceTests : PhoneValidatorServiceTestsFixture
{
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnSuccess_WhenPhoneNumberIsValid()
    {
        // Arrange
        SetupHttpClientFactory(HttpStatusCode.OK,  true);

        // Act
        var result = await PhoneValidatorService.ValidatePhoneNumberAsync(PhoneNumber);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnError_WhenPhoneNumberIsNotValid()
    {
        // Arrange
        SetupHttpClientFactory(HttpStatusCode.OK, false);
        
        // Act
        var result = await PhoneValidatorService.ValidatePhoneNumberAsync(PhoneNumber);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnError_WhenStatusCodeIsNotOk()
    {
        // Arrange
        SetupHttpClientFactory(HttpStatusCode.BadRequest, false);
        
        // Act
        var result = await PhoneValidatorService.ValidatePhoneNumberAsync(PhoneNumber);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnError_WhenResponseIsNull()
    {
        // Arrange
        SetupHttpClientFactory(HttpStatusCode.OK, false, true);
        
        // Act
        var result = await PhoneValidatorService.ValidatePhoneNumberAsync(PhoneNumber);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnError_WhenHttpClientThrows()
    {
        // Arrange
        SetupHttpClientFactoryWithThrow();
        
        // Act
        var result = await PhoneValidatorService.ValidatePhoneNumberAsync(PhoneNumber);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
}