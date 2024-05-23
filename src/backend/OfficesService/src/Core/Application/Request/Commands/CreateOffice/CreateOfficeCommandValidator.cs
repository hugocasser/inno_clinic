using Application.Abstractions.Services;
using FluentValidation;

namespace Application.Request.Commands.CreateOffice;

public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
{
    public CreateOfficeCommandValidator(IPhoneValidatorService phoneValidator, IGoogleMapsApiClient googleMapsApiClient)
    {
        RuleFor(x => x.Address).NotEmpty()
            .WithMessage(ErrorMessages.AddressCannotBeNullOrEmpty)
            .MustAsync(async (address, token) =>
            {
                var result = await googleMapsApiClient.ValidateAddressAsync(address, token);
                return result;
            })
            .WithMessage(ErrorMessages.AddressNotValid);
        
        RuleFor(x => x.RegistryPhoneNumber).NotEmpty()
            .WithMessage(ErrorMessages.PhoneCannotBeNullOfEmpty)
            .MustAsync(async (phoneNumber, token) =>
            {
                var result = await phoneValidator.ValidatePhoneNumberAsync(phoneNumber, token);
                return result;
            })
            .WithMessage(ErrorMessages.PhoneNumberNotValid);
    }
}