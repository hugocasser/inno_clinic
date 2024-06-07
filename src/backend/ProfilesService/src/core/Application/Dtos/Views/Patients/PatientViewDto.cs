using Application.Common;
using Domain.Models;

namespace Application.Dtos.Views.Patients;

public record PatientViewDto(Guid Id, string FullName, Guid PhotoId = default)
{
    public static PatientViewDto MapFromModel(Patient patient)
    {
        var valueStingBuilder = new ValueStringBuilder(stackalloc char[256]);
        
        valueStingBuilder.Append(patient.LastName);
        valueStingBuilder.Append(' ');
        valueStingBuilder.Append(patient.FirstName);

        if (!string.IsNullOrEmpty(patient.MiddleName))
        {
            valueStingBuilder.Append(' ');
            valueStingBuilder.Append(patient.MiddleName);
        }

        return new PatientViewDto(patient.Id, valueStingBuilder.ToString(), patient.PhotoId);
    }
};