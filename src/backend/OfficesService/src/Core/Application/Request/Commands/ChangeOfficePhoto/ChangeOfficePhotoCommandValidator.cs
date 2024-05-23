using FluentValidation;

namespace Application.Request.Commands.ChangeOfficePhoto;

public class ChangeOfficePhotoCommandValidator : AbstractValidator<ChangeOfficePhotoCommand>
{
    public ChangeOfficePhotoCommandValidator()
    {
        RuleFor(x => x.OfficeId).NotEmpty();
        RuleFor(x => x.PhotoId).NotEmpty();
    }
}