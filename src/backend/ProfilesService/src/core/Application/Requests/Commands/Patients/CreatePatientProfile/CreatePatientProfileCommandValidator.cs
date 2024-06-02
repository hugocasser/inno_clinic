using FluentValidation;

namespace Application.Requests.Commands.Patients.CreatePatientProfile;

public class CreatePatientProfileCommandValidator : AbstractValidator<CreatePatientProfileCommand>
{
    public CreatePatientProfileCommandValidator(){}
}