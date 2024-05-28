using Application.Abstractions.Services.ValidationServices;
using Application.Request.Commands.CreateOffice;
using Google.Type;

namespace UnitTests.Validators;

public class CreateOfficeCommandValidatorTests
{
    private readonly Mock<IGoogleMapsApiClient> _googleMapsApiClientMock = new();
    private readonly Mock<IPhoneValidatorService> _phoneValidatorServiceMock = new();
    
    [Theory]
    [InlineData("123456789", true)]
    [InlineData("123456789", false)]
    public async Task Validate_ShouldNotHaveValidationError(string phoneNumber, bool active)
    {
        // Arrange
        _googleMapsApiClientMock
            .Setup(x => x.ValidateAddressAsync(It.IsAny<PostalAddress>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success());
        _phoneValidatorServiceMock
            .Setup(x => x.ValidatePhoneNumberAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success());
        
        var validator = new CreateOfficeCommandValidator(_phoneValidatorServiceMock.Object, _googleMapsApiClientMock.Object);
        
        var command = new CreateOfficeCommand(Utilities.GenerateAddress(), phoneNumber, active, Guid.NewGuid());
        // Act
        
        var result = await validator.ValidateAsync(command);
        
        // Assert
        result.Errors.Should().BeEmpty();
    }
    
    [Theory]
    [InlineData("123456789", true)]
    [InlineData("123456789", false)]
    public async Task Validate_ShouldHaveValidationError_WhenAddressIsNotValid(string phoneNumber, bool active)
    {
        // Arrange
        _googleMapsApiClientMock
            .Setup(x => x.ValidateAddressAsync(It.IsAny<PostalAddress>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.BadRequest("Address is not valid"));
        _phoneValidatorServiceMock
            .Setup(x => x.ValidatePhoneNumberAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success());
        
        var validator = BuildValidator();
        
        var command = new CreateOfficeCommand(Utilities.GenerateAddress(), phoneNumber, active, Guid.NewGuid());
        // Act
        
        var result = await validator.ValidateAsync(command);
        
        // Assert
        result.Errors.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Validate_ShouldHaveValidationError_WhenPhoneNumberIsNotValid()
    {
        // Arrange
        _googleMapsApiClientMock
            .Setup(x => x.ValidateAddressAsync(It.IsAny<PostalAddress>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success());
        _phoneValidatorServiceMock
            .Setup(x => x.ValidatePhoneNumberAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.BadRequest("Phone number is not valid"));
        
        var validator = BuildValidator();
        
        var command = new CreateOfficeCommand(Utilities.GenerateAddress(), "123456789", true, Guid.NewGuid());
        // Act
        
        var result = await validator.ValidateAsync(command);
        
        // Assert
        result.Errors.Should().NotBeEmpty();
    }
    
    [Fact]
    public async Task Validate_ShouldHaveValidationError_WhenAddressIsNotValidAndPhoneNumberIsNotValid()
    {
        // Arrange
        _googleMapsApiClientMock
            .Setup(x => x.ValidateAddressAsync(It.IsAny<PostalAddress>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.BadRequest("Address is not valid"));
        _phoneValidatorServiceMock
            .Setup(x => x.ValidatePhoneNumberAsync(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.BadRequest("Phone number is not valid"));

        var validator = BuildValidator();
        
        var command = new CreateOfficeCommand(Utilities.GenerateAddress(), "123456789", true, Guid.NewGuid());
        // Act
        
        var result = await validator.ValidateAsync(command);
        
        // Assert
        result.Errors.Should().NotBeEmpty();
    }
    
    private CreateOfficeCommandValidator BuildValidator()
    {
        return new CreateOfficeCommandValidator(_phoneValidatorServiceMock.Object, _googleMapsApiClientMock.Object);
    }
}