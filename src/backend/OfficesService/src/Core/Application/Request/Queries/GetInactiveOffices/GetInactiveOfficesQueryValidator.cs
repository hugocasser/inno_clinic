using Application.Common;
using FluentValidation;

namespace Application.Request.Queries.GetInactiveOffices;

public class GetInactiveOfficesQueryValidator : AbstractValidator<GetInactiveOfficesQuery>
{
    public GetInactiveOfficesQueryValidator()
    {
        RuleFor(query => query.PageSettings).PageSettings();
    } 
}