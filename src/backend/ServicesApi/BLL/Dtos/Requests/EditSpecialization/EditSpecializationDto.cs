using DLL.Models;

namespace BLL.Dtos.Requests.EditSpecialization;

public record EditSpecializationDto(Guid Id, bool IsActive, string Name)
{
    public Specialization MapToModel()
    {
        return new Specialization
        {
            Id = Id,
            Name = Name,
            IsActive = IsActive
        };
    }
};