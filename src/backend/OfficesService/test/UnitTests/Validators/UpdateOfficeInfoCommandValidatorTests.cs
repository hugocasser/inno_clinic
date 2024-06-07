using Application.Abstractions.Services.ValidationServices;
using Application.Request.Commands.UpdateOfficeInfo;
using FluentValidation.TestHelper;
using Google.Type;

namespace UnitTests.Validators;

public class UpdateOfficeInfoCommandValidatorTests
{
    private readonly Mock<IPhoneValidatorService> _phoneValidatorServiceMock = new();
    private readonly Mock<IGoogleMapsApiClient> _googleMapsApiClientMock = new();
    private const string PhoneNumber = "123456789";
    [Fact]
    public async Task Validate_ShouldNotHaveValidationError()
    {
        // Arrange
        _phoneValidatorServiceMock
            .Setup(x => x.ValidatePhoneNumberAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success());
        _googleMapsApiClientMock
            .Setup(x => x.ValidateAddressAsync(It.IsAny<PostalAddress>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success());

        var validator = BuildValidator();
        var address = Utilities.GenerateAddress();
        var command = new UpdateOfficeInfoCommand(Guid.NewGuid(), address, PhoneNumber);
        
        // Act
        var result = await validator.ValidateAsync(command);
        
        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Fact]
    public async Task Validate_ShouldHaveValidationError_WhenAddressIsNotValid()
    {
        // Arrange
        _phoneValidatorServiceMock
            .Setup(x => x.ValidatePhoneNumberAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success());
        _googleMapsApiClientMock
            .Setup(x => x.ValidateAddressAsync(It.IsAny<PostalAddress>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.BadRequest("Address is not valid"));
        
        var validator = BuildValidator();
        var address = Utilities.GenerateAddress();
        var command = new UpdateOfficeInfoCommand(Guid.NewGuid(), address, PhoneNumber);
        
        // Act
        var result = await validator.ValidateAsync(command);
        
        // Assert
        result.Errors.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Validate_ShouldHaveValidationError_WhenPhoneNumberIsNotValid()
    {
        // Arrange
        _phoneValidatorServiceMock
            .Setup(x => x.ValidatePhoneNumberAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.BadRequest("Phone number is not valid"));
        _googleMapsApiClientMock
            .Setup(x => x.ValidateAddressAsync(It.IsAny<PostalAddress>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success());
        
        var validator = BuildValidator();
        var address = Utilities.GenerateAddress();
        var command = new UpdateOfficeInfoCommand(Guid.NewGuid(), address, PhoneNumber);
        
        // Act
        var result = await validator.ValidateAsync(command);
        
        // Assert
        result.Errors.Should().NotBeEmpty();
    }
    
    
    
    private UpdateOfficeInfoCommandValidator BuildValidator()
    {
        return new UpdateOfficeInfoCommandValidator(_googleMapsApiClientMock.Object, _phoneValidatorServiceMock.Object);
    }
}