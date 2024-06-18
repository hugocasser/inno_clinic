using Application.Abstractions;
using Application.Results;
using Presentation.Abstractions.Services;

namespace Presentation.Services;

public class StatusesMapperService : IStatusesMapperService
{
    private readonly Dictionary<Guid, string> _statuses = new()
    {
        { Guid.Parse("4ed76a26-4189-4823-bf82-7d04a5ba1f90"), "Active" },
        { Guid.Parse("ac867c61-7eb4-48f5-8300-b5021bdfc092"), "Not Active" },
    };
    
    public Task<IResult> GetStatusNameByIdAsync(Guid id)
    {
        var random = new Random().Next(1,2);
        var result = new Result();
        id = Guid.Parse(random == 1 ?
            "ac867c61-7eb4-48f5-8300-b5021bdfc092" :
            "4ed76a26-4189-4823-bf82-7d04a5ba1f90");

        _statuses.TryGetValue(id, out var name);
        
        result.SetResultData(name);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetStatusIdByNameAsync(string name)
    {
        var result = new Result();
        var id = _statuses.FirstOrDefault(x => x.Value == name).Key;
        
        result.SetResultData(id);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetAllStatusesAsync()
    {
        var result = new Result();
        result.SetResultData(_statuses);
        
        return Task.FromResult<IResult>(result);
    }
}