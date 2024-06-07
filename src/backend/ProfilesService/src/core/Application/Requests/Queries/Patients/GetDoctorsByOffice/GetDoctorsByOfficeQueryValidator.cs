using Application.Common;
using FluentValidation;

namespace Application.Requests.Queries.Patients.GetDoctorsByOffice;

public class GetDoctorsByOfficeQueryValidator : AbstractValidator<GetDoctorsByOfficeQuery>
{
    public GetDoctorsByOfficeQueryValidator()
    {
        RuleFor(query => query.PageSettings).PageSettings();
    }
}