using System.Text;
using Application.Common;
using Domain.Models;

namespace Application.Dtos.Views;

public record ReceptionistWithoutPhotoViewDto(Guid Id, string FullName)
{
    public static ReceptionistWithoutPhotoViewDto MapFromModel(Receptionist model)
    {
        var valueStringBuilder = new ValueStringBuilder();

        valueStringBuilder.Append(model.FirstName);
        valueStringBuilder.Append(" ");
        valueStringBuilder.Append(model.LastName);

        if (string.IsNullOrWhiteSpace(model.MiddleName))
        {
            return new ReceptionistWithoutPhotoViewDto(model.Id, valueStringBuilder.ToString());
        }

        valueStringBuilder.Append(" ");
        valueStringBuilder.Append(model.MiddleName);

        return new ReceptionistWithoutPhotoViewDto(model.Id, valueStringBuilder.ToString());
    }
};