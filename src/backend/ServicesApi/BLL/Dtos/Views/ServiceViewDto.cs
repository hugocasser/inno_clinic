using DLL.Models;

namespace BLL.Dtos.Views;

public record ServiceViewDto(Guid Id, string Name, Guid SpecializationId, Guid CategoryId, bool IsActive, int Price)
{
    public static ServiceViewDto FromModel(Service model)
    {
        return new ServiceViewDto(model.Id, model.Name, model.SpecializationId, model.CategoryId, model.IsActive, model.Price);
    }
};