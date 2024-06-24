using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Results;

namespace Infrastructure.Services;

public class StatusesService : IStatusesService
{
    public Task<IResult> GetStatusNameByIdAsync(Guid id)
    {
        var result = new Result();
        var data = new SpecializationViewDto(Guid.NewGuid(), "Dermatology");
        result.SetResultData(data);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetAllStatusesAsync()
    {
        var result = new Result();
        var data = new List<StatusViewDto>()
        {
            new(Guid.NewGuid(), "Active"),
            new(Guid.NewGuid(), "Inactive")
        };
        result.SetResultData(data);
        
        return Task.FromResult<IResult>(result);
    }
}