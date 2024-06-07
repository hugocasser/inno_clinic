using Application.Common;
using FluentValidation;

namespace Application.Requests.Queries.Patients.GetDoctorsAsPatient;

public class GetDoctorsAsPatientQueryValidator : AbstractValidator<GetDoctorsAsPatientQuery>
{
    public GetDoctorsAsPatientQueryValidator()
    {
        RuleFor(query => query.PageSettings).PageSettings();
    }
}