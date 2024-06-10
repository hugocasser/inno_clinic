using BusinessLayer.Abstractions.Services;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Repositories;
using DataAccess.Dtos;
using DataAccess.Models;
using DataAccess.OperationResult;
using DataAccess.OperationResult.Abstractions;
using DataAccess.OperationResult.Errors;
using DataAccess.OperationResult.Resources;

namespace BusinessLayer.Services;

public class FilesService
    (IBlobService blobService,
        IBlobFileInfoRepository blobFileInfoRepository) : IFilesService
{
    public async Task<IResult> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken)
    {
        var result = await blobService.UploadAsync(stream, contentType, cancellationToken);

        if (!result.IsSuccess)
        {
            return result;
        }

        var fileInfo = new BlobFileInfo
        {
            Id = result.GetTypedContent<Guid>(),
            ContentType = contentType,
            UploadedDate = DateOnly.FromDateTime(DateTime.UtcNow),
            Size = (int)stream.Length,
            LastDownloadDate = DateOnly.FromDateTime(DateTime.UtcNow),
            IsDeleted = false,
            IsDeletedFromBlobStorage = false
        };
            
        await blobFileInfoRepository.CreateAsync(fileInfo, cancellationToken);
        await blobFileInfoRepository.SaveChangesAsync(cancellationToken);
            
        return ResultBuilder.Success();
    }

    public async Task<IResult> GetByIdAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var fileInfo = await blobFileInfoRepository.GetByIdAsync(fileId, cancellationToken);

        if (fileInfo is null)
        {
            return ResultBuilder.Failure(new Error(ResultsMessages.FileNotFound));
        }
        
        var result = await blobService.GetByIdAsync(fileId, cancellationToken);
        
        if (!result.IsSuccess)
        {
            return ResultBuilder.Failure(result.GetTypedContent<ICollection<Error>>());
        }

        fileInfo.LastDownloadDate = DateOnly.FromDateTime(DateTime.UtcNow);
        
        await blobFileInfoRepository.UpdateAsync(fileInfo);
        await blobFileInfoRepository.SaveChangesAsync(cancellationToken);
        
        return ResultBuilder.Success(result.GetTypedContent<FileResponse>());
    }

    public async Task<IResult> RollBackUploadAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var fileInfo = await blobFileInfoRepository.GetByIdFromDeletedAsync(fileId, cancellationToken);
        
        if (fileInfo is null)
        {
            return ResultBuilder.Failure(new Error("File not found"));
        }
        
        await blobService.DeleteAsync(fileInfo.Id, cancellationToken);
        await blobFileInfoRepository.DeleteFromBoothStoragesAsync(fileInfo.Id, cancellationToken);
        await blobFileInfoRepository.SaveChangesAsync(cancellationToken);
        
        return ResultBuilder.Success();
    }

    public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var fileInfo =await blobFileInfoRepository.GetByIdAsync(fileId, cancellationToken);
        
        if (fileInfo is null)
        {
            return;
        }
        
        await blobFileInfoRepository.DeleteFromBoothStoragesAsync(fileInfo.Id, cancellationToken);
        await blobFileInfoRepository.DeleteAsync(fileInfo.Id, cancellationToken);
        
        await blobFileInfoRepository.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> IsExistsAsync(Guid fileId, CancellationToken cancellationToken)
    {
        return await blobFileInfoRepository.IsExistsAsync(fileId, cancellationToken);
    }
}