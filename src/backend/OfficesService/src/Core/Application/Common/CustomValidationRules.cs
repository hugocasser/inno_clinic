using Application.Abstractions.Services;
using Application.Abstractions.Services.ValidationServices;
using Application.Dtos.Requests;
using FluentValidation;
using Google.Maps.AddressValidation.V1;
using Google.Type;

namespace Application.Common;

public static class CustomValidationRules
{
    public static IRuleBuilderOptions<T, PostalAddress> Address<T>(this IRuleBuilder<T, PostalAddress> ruleBuilder, IGoogleMapsApiClient googleMapsApiClient)
    {        
        return 
            ruleBuilder
            .NotNull().WithMessage(ErrorMessages.AddressCannotBeNullOrEmpty)
            .MustAsync(async (address, token) =>
                {
                    var result = await googleMapsApiClient.ValidateAddressAsync(address, token);
                    
                     return result.IsSuccess;
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
                    
                    return result.IsSuccess;
                }).WithMessage(ErrorMessages.AddressNotValid);
    }
    
    public static IRuleBuilder<T, PageSettings> PageSettings<T>(this IRuleBuilder<T, PageSettings> ruleBuilder)
    {
        ruleBuilder
            .NotNull().WithMessage(ErrorMessages.PageSettingCannotBeNull);
        
        return ruleBuilder
            .Must(x => x.Page > 0).WithMessage(ErrorMessages.PageMustBeGrateWhenZero)
            .Must(x => x.ItemsPerPage > 0).WithMessage(ErrorMessages.PageSizeMustBeGraterThenZero);
    }
}