using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ExternalServices;
using Application.OperationResults;

namespace Application.Services.ExternalServices;

public class PhotoService : IPhotoService
{
    public Task<IResult> UploadPhotoInBase64Async(Guid photoId, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success());
    }
}