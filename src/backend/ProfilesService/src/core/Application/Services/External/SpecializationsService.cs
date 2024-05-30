using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ExternalServices;
using Application.OperationResult.Builders;

namespace Application.Services.External;

public class SpecializationsService : ISpecializationsService
{
    //TODO: implement specialization service
    // now it's just mock while i don't implement services communication
    public Task<IResult> CheckSpecializationAsync(Guid specializationId, CancellationToken cancellationToken)
    {
        return Task.FromResult(OperationResultBuilder.Success() as IResult);
    }
}