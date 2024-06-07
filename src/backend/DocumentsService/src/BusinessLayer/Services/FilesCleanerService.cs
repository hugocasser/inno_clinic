using BusinessLayer.Abstractions.Services;
using DataAccess.Abstractions;
using DataAccess.Abstractions.Repositories;
using Microsoft.Extensions.Logging;

namespace BusinessLayer.Services;

public class FilesCleanerService
    (IBlobService blobService,
        IBlobFileInfoRepository blobFileInfoRepository,
        ILogger<FilesCleanerService> logger)
    : IFilesCleanerService
{
    public async Task CleanAsync(CancellationToken cancellationToken)
    {
        logger.LogInformation("Cleaning unused files");
        
        await foreach(var fileInfo in blobFileInfoRepository.GetUnusedFilesAsync(100, cancellationToken))
        {
            logger.LogInformation("Deleting unused file {FileId}", fileInfo.Id);
            
            await blobService.DeleteAsync(fileInfo.Id, cancellationToken);
            await blobFileInfoRepository.DeleteFromBoothStoragesAsync(fileInfo.Id, cancellationToken);
            
            logger.LogInformation("Deleted unused file {FileId}", fileInfo.Id);
        }
        
        await blobFileInfoRepository.SaveChangesAsync(cancellationToken);
        
        logger.LogInformation("Finished cleaning unused files");
    }
}