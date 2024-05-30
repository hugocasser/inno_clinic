using Application.Abstractions.OperationResult;

namespace Application.Abstractions.Services.ExternalServices;

public interface IOfficesService
{
    public Task<IResult> CheckOfficeAsync(Guid officeId, CancellationToken cancellationToken);
}