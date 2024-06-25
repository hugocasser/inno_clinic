using BLL.Resources;
using FluentValidation;

namespace BLL.Dtos.Requests.EditSpecialization;

public class EditSpecializationDtoValidator : AbstractValidator<EditSpecializationDto>
{
    public EditSpecializationDtoValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
    }
}