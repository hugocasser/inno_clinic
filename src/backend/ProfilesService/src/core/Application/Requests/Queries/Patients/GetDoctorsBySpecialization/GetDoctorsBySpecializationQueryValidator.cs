using Application.Common;
using FluentValidation;

namespace Application.Requests.Queries.Patients.GetDoctorsBySpecialization;

public class GetDoctorsBySpecializationQueryValidator : AbstractValidator<GetDoctorsBySpecializationQuery>
{
    public GetDoctorsBySpecializationQueryValidator()
    {
        RuleFor(query => query.PageSettings).PageSettings();
    }
}