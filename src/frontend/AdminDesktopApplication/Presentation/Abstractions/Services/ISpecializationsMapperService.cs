using Application.Abstractions;

namespace Presentation.Abstractions.Services;

public interface ISpecializationsMapperService
{
    public Task<IResult> GetAllSpecializationsAsync();
    public Task<IResult> GetSpecializationNameByIdAsync(Guid id);
    public Task<IResult> GetSpecializationIdByNameAsync(string name);
}