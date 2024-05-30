using Application.Abstractions.OperationResult;

namespace Application.Abstractions.Services.ExternalServices;

public interface IPhotoService
{
    public Task<IResult> CheckPhotoAsync(Guid? photoId, CancellationToken cancellationToken);
}