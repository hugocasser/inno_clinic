using Domain.Models;

namespace Application.Dtos.View;

public record OfficeWithPhotoViewDto(Guid Id, string? Address, string? RegistryPhoneNumber, string? PhotoBase64, bool IsActive)
{
    public static OfficeWithPhotoViewDto MapFromModel(Office? model, string? base64Photo)
    {
        return new OfficeWithPhotoViewDto(model.Id, model.Address, model.RegistryPhoneNumber, base64Photo, model.IsActive);
    }
}