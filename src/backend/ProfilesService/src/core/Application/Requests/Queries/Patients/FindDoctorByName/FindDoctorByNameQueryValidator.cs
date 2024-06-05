using Application.Common;
using Application.Requests.Resources;
using FluentValidation;

namespace Application.Requests.Queries.Patients.FindDoctorByName;

public class FindDoctorByNameQueryValidator : AbstractValidator<FindDoctorByNameQuery>
{
    public FindDoctorByNameQueryValidator()
    {
        RuleFor(query => query.Name)
            .MaximumLength(100)
            .WithMessage(ValidationErrorMessages.LastNameToLong);

        RuleFor(query => query.PageSettings).PageSettings();
    }
}