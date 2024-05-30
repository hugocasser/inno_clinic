using Application.Abstractions.OperationResult;

namespace Application.Abstractions.Services.ExternalServices;

public interface ISpecializationsService
{
    public Task<IResult> CheckSpecializationAsync(Guid specializationId, CancellationToken cancellationToken);
    public Task<string> GetSpecializationNameAsync(Guid specializationId, CancellationToken cancellationToken);
}