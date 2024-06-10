using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services.External;

public interface IFilesService
{
    Task<IResult> UploadFileAsync(IFormFile photo, CancellationToken cancellationToken);
    Task<IResult> TryRollbackAsync(Guid getContent, CancellationToken cancellationToken);
    Task<IResult> ReplaceFileAsync(IFormFile requestPhoto, Guid requestOldPhotoId, CancellationToken cancellationToken);
}