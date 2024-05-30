using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ExternalServices;
using Application.OperationResult.Builders;

namespace Application.Services.External;

public class PhotoService : IPhotoService
{
    //TODO: implement photo service
    // now it's just mock while i don't implement services communication
    public Task<IResult> CheckPhotoAsync(Guid? photoId, CancellationToken cancellationToken)
    {
        return Task.FromResult(OperationResultBuilder.Success() as IResult);
    }
}