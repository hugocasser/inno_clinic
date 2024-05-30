using Application.Abstractions.Services;
using Application.Abstractions.Services.ValidationServices;
using Application.Common;
using FluentValidation;

namespace Application.Request.Commands.CreateOffice;

public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
{
    public CreateOfficeCommandValidator(IPhoneValidatorService phoneValidator, IGoogleMapsApiClient googleMapsApiClient)
    {
        RuleFor(command => command.AddressRequestDto.ToPostalAddress()).Address(googleMapsApiClient);

        RuleFor(command => command.RegistryPhoneNumber)
            .Phone(phoneValidator);
        
        RuleFor(command => command.IsActive).NotNull();
        
        RuleFor(command => command.PhotoId).NotEmpty();
    }
}