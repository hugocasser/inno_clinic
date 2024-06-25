using DLL.Models;

namespace BLL.Dtos.Requests;

public record CreateServiceDto(
    string Name,
    Guid SpecializationId,
    Guid CategoryId,
    bool IsActive,
    int Price)
{
    public Service MapToModel()
    {
        return new Service
        {
            Id = Guid.NewGuid(),
            Name = Name,
            SpecializationId = SpecializationId,
            CategoryId = CategoryId,
            IsActive = IsActive,
            Price = Price
        };
    }
};