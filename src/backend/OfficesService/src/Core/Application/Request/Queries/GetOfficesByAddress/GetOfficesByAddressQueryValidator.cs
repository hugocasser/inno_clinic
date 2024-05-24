using Application.Common;
using FluentValidation;

namespace Application.Request.Queries.GetOfficesByAddress;

public class GetOfficesByAddressQueryValidator : AbstractValidator<GetOfficesByAddressQuery>
{
    public GetOfficesByAddressQueryValidator()
    {
        RuleFor(query => query.Address).NotEmpty();

        RuleFor(query => query.OnlyActive).NotNull();

        RuleFor(query => query.PageSettings).PageSettings();
    }
}