using Application.Abstractions;

namespace Presentation.Abstractions.Services.PipelinedService;

public interface IPipelinedStatusesService
{
    public Task<IResult> GetStatusNameByIdAsync(Guid id);
    public Task<IResult> GetAllStatusesAsync();
}