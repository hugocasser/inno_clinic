using Application.Abstractions;

namespace Presentation.Abstractions.Services.PipelinedService;

public interface IPipelinedOfficesService
{
    public Task<IResult> GetAllAsync();
}