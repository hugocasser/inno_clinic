using DLL.Models;

namespace BLL.Dtos.Views;

public record CategoryViewDto(Guid Id, string Name)
{
    public static CategoryViewDto MapFromModel(Category category)
    {
        return new CategoryViewDto
            (category.Id, category.Name);
    }
};