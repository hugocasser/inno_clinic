using Application.Abstractions.Services;
using Application.Abstractions.Services.ValidationServices;
using Application.Common;
using FluentValidation;

namespace Application.Request.Commands.UpdateOfficeInfo;

public class UpdateOfficeInfoCommandValidator : AbstractValidator<UpdateOfficeInfoCommand>
{
    public UpdateOfficeInfoCommandValidator(IGoogleMapsApiClient googleMapsApiClient,
        IPhoneValidatorService phoneValidator)
    {
        RuleFor(command => command.AddressRequestDto.ToPostalAddress()).Address(googleMapsApiClient);
        
        RuleFor(command => command.OfficeId).NotEmpty();
        
        RuleFor(command => command.RegistryPhoneNumber).Phone(phoneValidator);
    }
}