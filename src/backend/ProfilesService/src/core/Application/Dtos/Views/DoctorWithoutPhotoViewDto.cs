using Domain.Models;

namespace Application.Dtos.Views;

public record DoctorWithoutPhotoViewDto(Guid Id, string FullName)
{
    public static DoctorWithoutPhotoViewDto MapFromModel(Doctor doctor)
    {
        return new DoctorWithoutPhotoViewDto(doctor.Id, doctor.LastName + " " + doctor.FirstName + " " + doctor.MiddleName);
    }
};