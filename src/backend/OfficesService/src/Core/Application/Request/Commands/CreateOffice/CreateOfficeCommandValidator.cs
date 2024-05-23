using Application.Abstractions.Services;
using Application.Abstractions.Services.ValidationServices;
using Application.Common;
using FluentValidation;

namespace Application.Request.Commands.CreateOffice;

public class CreateOfficeCommandValidator : AbstractValidator<CreateOfficeCommand>
{
    public CreateOfficeCommandValidator(IPhoneValidatorService phoneValidator, IGoogleMapsApiClient googleMapsApiClient)
    {
        RuleFor(x => x.Address).Address(googleMapsApiClient);

        RuleFor(x => x.RegistryPhoneNumber).Phone(phoneValidator);
        
        RuleFor(x => x.IsActive).NotNull();
        
        RuleFor(x => x.PhotoId).NotEmpty();
    }
}