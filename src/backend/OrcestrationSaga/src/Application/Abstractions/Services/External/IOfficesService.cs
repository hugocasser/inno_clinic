namespace Application.Abstractions.Services.External;

public interface IOfficesService
{
    public Task<bool> IsOfficeExistAsync(Guid officeId, CancellationToken cancellationToken = default);
}