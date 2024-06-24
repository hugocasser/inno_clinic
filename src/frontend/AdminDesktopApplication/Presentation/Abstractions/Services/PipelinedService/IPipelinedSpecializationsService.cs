using Application.Abstractions;

namespace Presentation.Abstractions.Services.PipelinedService;

public interface IPipelinedSpecializationsService
{
    public Task<IResult> GetAllSpecializationsAsync();
    public Task<IResult> GetSpecializationNameByIdAsync(Guid id);
}