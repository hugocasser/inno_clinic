using DLL.Models;

namespace BLL.Dtos.Views;

public record SpecializationViewDto(Guid Id, string Name, bool IsActive)
{
    public static SpecializationViewDto MapFromModel(Specialization specialization)
    {
        return new SpecializationViewDto
            (specialization.Id, specialization.Name, specialization.IsActive);
    }
};