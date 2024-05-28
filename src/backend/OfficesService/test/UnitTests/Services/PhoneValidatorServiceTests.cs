using System.Net;
using Application.Dtos.Validation.PhoneValidation;

namespace UnitTests.Services;

public class PhoneValidatorServiceTests : PhoneValidatorServiceTestsFixture
{
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnSuccess_WhenPhoneNumberIsValid()
    {
        // Arrange
        var phoneValidatorResponse = new PhoneValidatorResponse
            (PhoneNumber, true, new Format("", ""), new Country("", "", ""),
                "", "", "");
        
        SetupHttpClientFactory(HttpStatusCode.OK, phoneValidatorResponse);

        // Act
        var result = await PhoneValidatorService.ValidatePhoneNumberAsync(PhoneNumber);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
    }
    
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnError_WhenPhoneNumberIsNotValid()
    {
        // Arrange
        var phoneValidatorResponse = new PhoneValidatorResponse
            (PhoneNumber, false, new Format("", ""), new Country("", "", ""),
                "", "", "");
        
        SetupHttpClientFactory(HttpStatusCode.OK, phoneValidatorResponse);
        
        // Act
        var result = await PhoneValidatorService.ValidatePhoneNumberAsync(PhoneNumber);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnError_WhenStatusCodeIsNotOk()
    {
        // Arrange
        var phoneValidatorResponse = new PhoneValidatorResponse
        (PhoneNumber, false, new Format("", ""), new Country("", "", ""),
            "", "", "");
        
        SetupHttpClientFactory(HttpStatusCode.BadRequest, phoneValidatorResponse);
        
        // Act
        var result = await PhoneValidatorService.ValidatePhoneNumberAsync(PhoneNumber);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
    }
    
    [Fact]
    public async Task ValidatePhoneNumber_ShouldReturnError_WhenResponseIsNull()
    {
        // Arrange
        SetupHttpClientFactory(HttpStatusCode.OK, null);
        
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