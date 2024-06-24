namespace Application.Abstractions.Services;

public interface IOfficesService
{
    public Task<IResult> GetAllAsync();
    public Task<IResult> GetByIdAsync(Guid id);
}