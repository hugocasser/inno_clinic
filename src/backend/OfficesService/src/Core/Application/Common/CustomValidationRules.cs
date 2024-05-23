using Application.Abstractions.Services;
using Application.Abstractions.Services.ValidationServices;
using FluentValidation;

namespace Application.Common;

public static class CustomValidationRules
{
    public static IRuleBuilderOptions<T, string> Address<T>(this IRuleBuilder<T, string> ruleBuilder, IGoogleMapsApiClient googleMapsApiClient)
    {        
        return 
            ruleBuilder
            .NotEmpty().WithMessage(ErrorMessages.AddressCannotBeNullOrEmpty)
            .MustAsync(async (address, token) =>
                {
                    var result = await googleMapsApiClient.ValidateAddressAsync(address, token);
                    
                     return result;
                }).WithMessage(ErrorMessages.AddressNotValid);
    }
    
    public static IRuleBuilderOptions<T, string> Phone<T>(this IRuleBuilder<T, string> ruleBuilder, IPhoneValidatorService phoneValidatorService)
    {        
        return 
            ruleBuilder
                .NotEmpty().WithMessage(ErrorMessages.PhoneCannotBeNullOfEmpty)
                .MustAsync(async (address, token) =>
                {
                    var result = await phoneValidatorService.ValidatePhoneNumberAsync(address, token);
                    
                    return result;
                }).WithMessage(ErrorMessages.AddressNotValid);
    }
}