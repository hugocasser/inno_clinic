using Application.Common;
using Domain.Models;

namespace Application.Dtos.Views;

public record PatientWithoutPhotoViewDto(Guid Id, string FullName)
{
    public static PatientWithoutPhotoViewDto MapFromModel(Patient patient)
    {
        var valueStingBuilder = new ValueStringBuilder(stackalloc char[256]);
        
        valueStingBuilder.Append(patient.LastName);
        valueStingBuilder.Append(' ');
        valueStingBuilder.Append(patient.FirstName);

        if (string.IsNullOrEmpty(patient.MiddleName))
        {
            return new PatientWithoutPhotoViewDto(patient.Id, valueStingBuilder.ToString());
        }

        valueStingBuilder.Append(' ');
        valueStingBuilder.Append(patient.MiddleName);

        return new PatientWithoutPhotoViewDto(patient.Id, valueStingBuilder.ToString());
    }
};