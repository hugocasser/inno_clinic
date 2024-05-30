using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ExternalServices;
using Application.OperationResult.Builders;

namespace Application.Services.External;

public class OfficesService : IOfficesService
{
    //TODO: implement offices service
    // now it's just mock while i don't implement services communication
    public Task<IResult> CheckOfficeAsync(Guid officeId, CancellationToken cancellationToken)
    {
        return Task.FromResult(OperationResultBuilder.Success() as IResult);
    }
}