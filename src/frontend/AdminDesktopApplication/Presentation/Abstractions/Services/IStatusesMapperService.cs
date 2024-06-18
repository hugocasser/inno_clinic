using Application.Abstractions;

namespace Presentation.Abstractions.Services;

public interface IStatusesMapperService
{
    public Task<IResult> GetStatusNameByIdAsync(Guid id);
    public Task<IResult> GetStatusIdByNameAsync(string name);
    public Task<IResult> GetAllStatusesAsync();
}