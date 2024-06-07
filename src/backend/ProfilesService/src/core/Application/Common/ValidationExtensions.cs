using Application.Dtos.Requests;
using Application.Requests.Resources;
using FluentValidation;

namespace Application.Common;

public static class ValidationExtensions
{
    public static IRuleBuilderOptions<T, PageSettings> PageSettings<T>(this IRuleBuilder<T, PageSettings> ruleBuilder)
    {
        return ruleBuilder
            .NotNull().WithMessage(ValidationErrorMessages.PageSettingsAreRequred)
            .Must(pageSettings => pageSettings.PageSize > 0).WithMessage(ValidationErrorMessages.PageSizeMustBeGraterThanZero)
            .Must(pageSettings => pageSettings.PageNumber > 0).WithMessage(ValidationErrorMessages.PageNumberMustBeGraterThanZero);
    }
}