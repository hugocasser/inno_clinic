namespace Application.Abstractions.Services;

public interface IOfficesService
{
    public Task<bool> IsOfficeExist(Guid officeId);
}