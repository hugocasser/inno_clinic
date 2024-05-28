using Domain.Models;

namespace Application.Dtos.View;

public record OfficeWithoutPhotoViewDto (Guid Id, string? Address, string? RegistryPhoneNumber, bool IsActive)
{
    public static OfficeWithoutPhotoViewDto MapFromModel(Office model)
    {
        return new OfficeWithoutPhotoViewDto(model.Id, model.Address, model.RegistryPhoneNumber, model.IsActive);
    }
};