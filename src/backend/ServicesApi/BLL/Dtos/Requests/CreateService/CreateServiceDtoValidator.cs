using BLL.Resources;
using FluentValidation;

namespace BLL.Dtos.Requests.CreateService;

public class CreateServiceDtoValidator : AbstractValidator<CreateServiceDto>
{
    public CreateServiceDtoValidator()
    {
        RuleFor(x => x.Name)
            .Must((x, name) => !string.IsNullOrWhiteSpace(name))
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
        
        RuleFor(x => x.SpecializationId)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
        RuleFor(x => x.Price).GreaterThan(0)
            .WithMessage(ValidationMessages.MustBeGreaterThen + " 0");
    }
}