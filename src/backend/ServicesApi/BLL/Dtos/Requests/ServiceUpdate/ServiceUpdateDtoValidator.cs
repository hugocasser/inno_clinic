using BLL.Resources;
using FluentValidation;

namespace BLL.Dtos.Requests.ServiceUpdate;

public class ServiceUpdateDtoValidator : AbstractValidator<ServiceUpdateDto>
{
    public ServiceUpdateDtoValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        
        RuleFor(x => x.Name)
            .Must((name) => !string.IsNullOrWhiteSpace(name))
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
        
        RuleFor(x => x.SpecializationId)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
        
        RuleFor(x => x.CategoryId)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
        
        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage(ValidationMessages.MustBeGreaterThen + " 0");
    }
}