using Application.Abstractions;
using Application.Abstractions.Services;
using Application.Dtos;
using Application.Results;

namespace Infrastructure.Services;

public class OfficesService : IOfficesService
{
    public Task<IResult> GetAllAsync()
    {
        var officesList = new List<OfficeViewDto>()
        {
            new(Guid.NewGuid(), "Address 1"),
            new(Guid.NewGuid(), "Address 2")
        };
        
        var result = new Result();
        result.SetResultData(officesList);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetByIdAsync(Guid id)
    {
        var office = new OfficeViewDto(id, "Address");
        
        var result = new Result();
        result.SetResultData(office);
        
        return Task.FromResult<IResult>(result);
    }
}