using DLL.Models;

namespace BLL.Dtos.Views;

public record SpecializationListItemViewDto(Guid Id, string Name, bool IsActive)
{
    public static SpecializationListItemViewDto MapFromModel(Specialization specialization)
    {
        return new SpecializationListItemViewDto
            (specialization.Id, specialization.Name, specialization.IsActive);
    }
}