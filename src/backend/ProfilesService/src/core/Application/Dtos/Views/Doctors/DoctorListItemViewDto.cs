using Application.ReadModels;

namespace Application.Dtos.Views.Doctors;

public record DoctorListItemViewDto(Guid Id, string FullName, string Specialization)
{
    public static List<DoctorListItemViewDto> MapFromReadModels(IEnumerable<DoctorReadModel> readModels)
    {
        return readModels
            .Select(x => new DoctorListItemViewDto(x.Id, x.FullName, x.Specialization))
            .ToList();
    }
};