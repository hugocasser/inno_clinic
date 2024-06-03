using Application.Common;
using Domain.Models;

namespace Application.Dtos.Views;

public record DoctorWithoutPhotoViewDto(Guid Id, string FullName)
{
    public static DoctorWithoutPhotoViewDto MapFromModel(Doctor doctor)
    {
        var valueStingBuilder = new ValueStringBuilder(stackalloc char[256]);
        
        valueStingBuilder.Append(doctor.LastName);
        valueStingBuilder.Append(' ');
        valueStingBuilder.Append(doctor.FirstName);
        
        return new DoctorWithoutPhotoViewDto(doctor.Id, doctor.LastName + " " + doctor.FirstName + " " + doctor.MiddleName);
    }
};