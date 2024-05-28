using Application.Common;
using FluentValidation;

namespace Application.Request.Queries.GetOfficesByNumber;

public class GetOfficesByNumberQueryValidator : AbstractValidator<GetOfficesByNumberQuery>
{
    public GetOfficesByNumberQueryValidator()
    {
        RuleFor(query => query.Number).NotEmpty();

        RuleFor(query => query.OnlyActive).NotNull();

        RuleFor(query => query.PageSettings).PageSettings();
    }
}