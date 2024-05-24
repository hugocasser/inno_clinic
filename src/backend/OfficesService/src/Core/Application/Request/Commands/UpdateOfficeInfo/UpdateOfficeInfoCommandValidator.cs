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
        RuleFor(x => x.AddressRequestDto.ToPostalAddress()).Address(googleMapsApiClient);
        
        RuleFor(x => x.OfficeId).NotEmpty();
        
        RuleFor(x => x.RegistryPhoneNumber).Phone(phoneValidator);
    }
}