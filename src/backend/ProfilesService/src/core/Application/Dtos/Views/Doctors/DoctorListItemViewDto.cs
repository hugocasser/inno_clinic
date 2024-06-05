using Application.ReadModels;

namespace Application.Dtos.Views.Doctors;

public record DoctorListItemViewDto(Guid Id, string FullName, string Specialization)
{
    public static DoctorListItemViewDto MapFromReadModel(DoctorReadModel readModel)
    {
        return new DoctorListItemViewDto(readModel.Id, readModel.FullName, readModel.Specialization);
    }
};