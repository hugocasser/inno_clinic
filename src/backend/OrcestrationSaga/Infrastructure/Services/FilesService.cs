using Application.Abstractions;
using Application.Abstractions.Services.External;
using Application.Result;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Services;

public class FilesService : IFilesService
{
    //TODO: add logic to upload file
    // now it's just mock while services communication not implemented
    public Task<IResult> UploadFileAsync(IFormFile? photo, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }

    public Task<IResult> TryRollbackAsync(Guid getContent, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }

    public Task<IResult> ReplaceFileAsync(IFormFile? requestPhoto, Guid requestOldPhotoId, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }

    public Task<IResult> DeletePhotoAsync(Guid photoId, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }
}