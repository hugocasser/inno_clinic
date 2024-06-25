using BLL.Result;

namespace BLL.Abstractions.Services;

public interface ICategoriesService
{
    public Task<OperationResult> GetAllAsync(CancellationToken cancellationToken);
}