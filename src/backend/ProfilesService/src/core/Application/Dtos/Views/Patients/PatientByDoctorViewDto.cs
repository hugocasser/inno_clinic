using Application.ReadModels;

namespace Application.Dtos.Views.Patients;

public record PatientByDoctorViewDto(Guid Id, string FullName, string Birthday)
{
    public static PatientByDoctorViewDto MapFromModel(PatientReadModel patient)
    {
        return new PatientByDoctorViewDto(patient.Id, patient.FullName, patient.Birthday.ToString("dd.MM.yyyy"));
    }
};