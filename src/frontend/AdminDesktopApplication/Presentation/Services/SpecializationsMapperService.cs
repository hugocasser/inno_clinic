using Application.Abstractions;
using Application.Results;
using Presentation.Abstractions.Services;

namespace Presentation.Services;

public class SpecializationsMapperService : ISpecializationsMapperService
{
    private readonly Dictionary<Guid, string> _specializations = new()
    {
        { Guid.Parse("4ed76a26-4189-4823-bf82-7d04a5ba1f90"), "Specialization 1" },
        { Guid.Parse("ac867c61-7eb4-48f5-8300-b5021bdfc092"), "Specialization 2" },
    };
    
    public Task<IResult> GetAllSpecializationsAsync()
    {
        var result = new Result();
        result.SetResultData(_specializations);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetSpecializationNameByIdAsync(Guid id)
    {
        var result = new Result();
        var random = new Random().Next(1,2);

        id = Guid.Parse(random == 1 ? "ac867c61-7eb4-48f5-8300-b5021bdfc092" : "4ed76a26-4189-4823-bf82-7d04a5ba1f90");

        _specializations.TryGetValue(id, out var name);
        
        result.SetResultData(name);
        
        return Task.FromResult<IResult>(result);
    }

    public Task<IResult> GetSpecializationIdByNameAsync(string name)
    {
        var result = new Result();
        var id = _specializations.FirstOrDefault(x => x.Value == name).Key;
        
        result.SetResultData(id);
        
        return Task.FromResult<IResult>(result);
    }
}