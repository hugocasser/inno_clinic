namespace Application.Abstractions.Services;

public interface IStatusesService
{
    public Task<IResult> GetStatusNameByIdAsync(Guid id);
    public Task<IResult> GetAllStatusesAsync();
}