using FluentValidation;

namespace Application.Request.Commands.ChangeOfficePhoto;

public class ChangeOfficePhotoCommandValidator : AbstractValidator<ChangeOfficePhotoCommand>
{
    public ChangeOfficePhotoCommandValidator()
    {
        RuleFor(command => command.OfficeId).NotEmpty();
        RuleFor(command => command.PhotoId).NotEmpty();
    }
}