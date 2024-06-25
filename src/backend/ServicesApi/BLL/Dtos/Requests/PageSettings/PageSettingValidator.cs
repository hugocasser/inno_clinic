using BLL.Resources;
using FluentValidation;

namespace BLL.Dtos.Requests.PageSettings;

public class PageSettingValidator : AbstractValidator<PageSettings>
{
    public PageSettingValidator()
    {
        RuleFor(x => x.PageSize)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.MustBeGreaterThen + " 0");
        
        RuleFor(x => x.PageNumber)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.MustBeGreaterThen + " 0");
    }
}