using Application.Abstractions.Services.External;

namespace Infrastructure.Services;

public class OfficesService : IOfficesService
{
    //TODO: add logic to check if office exist
    // now it's just mock while services communication not implemented
    public Task<bool> IsOfficeExistAsync(Guid officeId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(true);
    }
}