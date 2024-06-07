using Application.Common;
using Domain.Models;

namespace Application.Dtos.Views.Receptionists;

public record ReceptionistViewDto(Guid Id, string FullName, Guid PhotoId = default)
{
    public static ReceptionistViewDto MapFromModel(Receptionist model)
    {
        var valueStringBuilder = new ValueStringBuilder(stackalloc char[256]);

        valueStringBuilder.Append(model.FirstName);
        valueStringBuilder.Append(" ");
        valueStringBuilder.Append(model.LastName);

        if (!string.IsNullOrWhiteSpace(model.MiddleName))
        {
            valueStringBuilder.Append(" ");
            valueStringBuilder.Append(model.MiddleName);
        }

        return new ReceptionistViewDto(model.Id, valueStringBuilder.ToString(), model.PhotoId);
    }
};