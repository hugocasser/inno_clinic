namespace Application.Abstractions.Services;

public interface ISpecializationsService
{
    public Task<IResult> GetAllSpecializationsAsync();
    public Task<IResult> GetSpecializationNameByIdAsync(Guid id);
}