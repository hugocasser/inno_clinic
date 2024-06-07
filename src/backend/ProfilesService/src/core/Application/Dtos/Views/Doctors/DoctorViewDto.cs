using Application.Common;
using Domain.Models;

namespace Application.Dtos.Views.Doctors;

public record DoctorViewDto(Guid Id, string FullName, Guid PhotoId = default)
{
    public static DoctorViewDto MapFromModel(Doctor doctor)
    {
        var valueStingBuilder = new ValueStringBuilder(stackalloc char[256]);
        
        valueStingBuilder.Append(doctor.LastName);
        valueStingBuilder.Append(' ');
        valueStingBuilder.Append(doctor.FirstName);
        
        if (!string.IsNullOrEmpty(doctor.MiddleName))
        {
            valueStingBuilder.Append(' ');
            valueStingBuilder.Append(doctor.MiddleName);
        }
        
        return new DoctorViewDto(doctor.Id, valueStingBuilder.ToString(), doctor.PhotoId);
    }
};