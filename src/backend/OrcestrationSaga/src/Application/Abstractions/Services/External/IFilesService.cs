using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services.External;

public interface IFilesService
{
    public Task<IResult> UploadFileAsync(IFormFile? photo, CancellationToken cancellationToken);
    public Task<IResult> TryRollbackAsync(Guid getContent, CancellationToken cancellationToken);
    public Task<IResult> ReplaceFileAsync(IFormFile? requestPhoto, Guid requestOldPhotoId, CancellationToken cancellationToken);
    public Task<IResult> DeletePhotoAsync(Guid photoId, CancellationToken cancellationToken);
}