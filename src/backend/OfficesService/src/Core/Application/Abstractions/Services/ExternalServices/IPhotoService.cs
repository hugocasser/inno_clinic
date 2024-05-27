using Application.Abstractions.OperationResult;

namespace Application.Abstractions.Services.ExternalServices;

public interface IPhotoService
{
    public Task<IResult> UploadPhotoInBase64Async(Guid? photoId, CancellationToken cancellationToken);
}