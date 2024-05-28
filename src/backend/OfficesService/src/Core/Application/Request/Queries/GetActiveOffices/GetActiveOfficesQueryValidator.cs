using Application.Common;
using FluentValidation;

namespace Application.Request.Queries.GetActiveOffices;

public class GetActiveOfficesQueryValidator : AbstractValidator<GetActiveOfficesQuery>
{
    public GetActiveOfficesQueryValidator()
    {
        RuleFor(query => query.PageSettings).PageSettings();
    }
}