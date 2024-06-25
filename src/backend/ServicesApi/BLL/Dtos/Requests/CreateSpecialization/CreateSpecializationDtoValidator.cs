using BLL.Resources;
using FluentValidation;

namespace BLL.Dtos.Requests.CreateSpecialization;

public class CreateSpecializationDtoValidator : AbstractValidator<CreateSpecializationDto>
{
    public CreateSpecializationDtoValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage(ValidationMessages.CannotBeNullOrEmpty);
    }
}