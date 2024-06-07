using Application.ReadModels;

namespace Application.Dtos.Views.Doctors;

public record DoctorByReceptionistViewDto
    (Guid Id, 
        string FullName,
        string DateOfBirth,
        string Specialization,
        Guid OfficeId,
        int YearStartedCareer,
        string Status)
{
    public static DoctorByReceptionistViewDto MapFormModel(DoctorReadModel model)
    {
        return new DoctorByReceptionistViewDto
            (model.Id,
                model.FullName,
                model.BirthDate.ToString("dd.MM.yyyy"),
                model.Specialization,
                model.OfficeId,
                model.CareerStarted.Year,
                model.Status);
    }
};