using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ExternalServices;
using Application.OperationResults;

namespace Application.Services.ExternalServices;

public class PhotoService : IPhotoService
{
    public Task<IResult> UploadPhotoInBase64Async(Guid? photoId, CancellationToken cancellationToken)
    {
        // TODO : upload photo in base64
        // now just return some string because photo service is not implemented
        return Task.FromResult(ResultBuilder.Success().WithData("some base64 string").WithStatusCode(200));
    }
}