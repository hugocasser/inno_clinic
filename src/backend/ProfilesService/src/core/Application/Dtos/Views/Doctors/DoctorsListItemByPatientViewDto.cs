using Application.ReadModels;

namespace Application.Dtos.Views.Doctors;

public record DoctorsListItemByPatientViewDto(Guid Id, string FullName, string Specialization)
{
    public static List<DoctorsListItemByPatientViewDto> MapFromReadModels(IEnumerable<DoctorReadModel> readModels)
    {
        return readModels
            .Select(x => new DoctorsListItemByPatientViewDto(x.Id, x.FullName, x.Specialization))
            .ToList();
    }
};