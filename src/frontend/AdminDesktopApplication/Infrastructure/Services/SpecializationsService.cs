using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Results;

namespace Infrastructure.Services;

public class SpecializationsService : ISpecializationsService
{
    public Task<IResult> GetAllSpecializationsAsync()
    {
        var result = new Result();
        var data = new List<SpecializationViewDto>()
        {
            new(Guid.NewGuid(), "Cardiology"),
            new(Guid.NewGuid(), "Neurology"),
            new(Guid.NewGuid(), "Dermatology")
        };
        result.SetResultData(data);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetSpecializationNameByIdAsync(Guid id)
    {
        var result = new Result();
        var data = new SpecializationViewDto(Guid.NewGuid(), "Dermatology");
        result.SetResultData(data);
        
        return Task.FromResult<IResult>(result);
    }
}