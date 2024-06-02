using Application.Abstractions.OperationResult;
using Application.OperationResult.Results;

namespace Application.Abstractions.Services.ExternalServices;

public interface IPhotoService
{
    public Task<IResult> CheckPhotoAsync(Guid? photoId, CancellationToken cancellationToken);
    public Task<OperationResult<bool>> DeletePhotoAsync(Guid patientPhotoId, CancellationToken cancellationToken);
}