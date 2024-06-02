using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ExternalServices;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Services.External;

public class PhotoService : IPhotoService
{
    //TODO: implement photo service
    // now it's just mock while i don't implement services communication
    public Task<IResult> CheckPhotoAsync(Guid? photoId, CancellationToken cancellationToken)
    {
        return Task.FromResult(OperationResultBuilder.Success() as IResult);
    }

    public Task<OperationResult<bool>> DeletePhotoAsync(Guid patientPhotoId, CancellationToken cancellationToken)
    {
        return Task.FromResult(OperationResultBuilder.Success());
    }
}