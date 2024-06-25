using BLL.Abstractions.Services;
using BLL.Dtos.Views;
using BLL.Result;
using DLL.Abstractions.Persistence.Repositories;

namespace BLL.Services;

public class CategoriesService(ICategoriesRepository categoriesRepository) : ICategoriesService
{
    public async Task<OperationResult> GetAllAsync(CancellationToken cancellationToken)
    {
        var result = await categoriesRepository.GetAllAsync();
        
        categoriesRepository.Commit();
        
        return ResultBuilder.Success(result.Select(CategoryViewDto.MapFromModel).ToList());
    }
}