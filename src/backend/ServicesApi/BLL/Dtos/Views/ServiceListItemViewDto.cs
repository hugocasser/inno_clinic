using DLL.Models;

namespace BLL.Dtos.Views;

public record ServiceListItemViewDto(Guid Id, string Name, bool IsActive, int Price)
{
    public static ServiceListItemViewDto FromModel(Service service)
    {
        return new ServiceListItemViewDto(service.Id, service.Name, service.IsActive, service.Price);
    }
}