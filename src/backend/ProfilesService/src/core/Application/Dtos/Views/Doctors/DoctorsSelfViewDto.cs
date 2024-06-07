using Application.ReadModels;

namespace Application.Dtos.Views.Doctors;

public record DoctorsSelfViewDto(
    Guid Id,
    string FullName,
    string DateOfBirth,
    int YearCariaStarted,
    Guid OfficeId,
    string Specialization,
    Guid PhotoId = default)
{
    public static DoctorsSelfViewDto MapFormModel(DoctorReadModel model)
    {
        return new DoctorsSelfViewDto(model.Id,
            model.FullName,
            model.BirthDate.ToString("dd.MM.yyyy"),
            model.CareerStarted.Year, model.OfficeId, model.Specialization, model.PhotoId);
    }
};