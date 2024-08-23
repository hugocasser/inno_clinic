using Application.Common;
using Application.Resources;
using FluentValidation;

namespace Application.Requests.Commands.EditResult;

public class EditResultCommandValidator : AbstractValidator<EditResultCommand>
{
    public EditResultCommandValidator()
    {
        RuleFor(command => command.Id)
            .NotEmptyWithMessage();
        
        RuleFor(command => command.Complaints)
            .ResultDescriptionString();
        
        RuleFor(command => command.Conclusion)
            .ResultDescriptionString();
        
        RuleFor(command => command.Recommendation)
            .ResultDescriptionString();
    }
}