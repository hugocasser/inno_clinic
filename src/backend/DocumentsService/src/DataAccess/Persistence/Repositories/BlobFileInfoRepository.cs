using DataAccess.Abstractions.Repositories;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Persistence.Repositories;

public class BlobFileInfoRepository(FileInfoDbContext context) : IBlobFileInfoRepository
{
    public async Task CreateAsync(BlobFileInfo fileInfo, CancellationToken cancellationToken)
    {
        await context.Set<BlobFileInfo>().AddAsync(fileInfo, cancellationToken);
    }

    public Task UpdateAsync(BlobFileInfo fileInfo)
    {
        context.Set<BlobFileInfo>().Update(fileInfo);
        
        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var fileInfo = await context.Set<BlobFileInfo>()
            .FirstOrDefaultAsync(file => file.FileId == fileId && !file.IsDeleted, cancellationToken);
        
        if (fileInfo != null)
        {
            fileInfo.IsDeleted = true;
            context.Set<BlobFileInfo>().Update(fileInfo);
        }
    }

    public async Task<BlobFileInfo?> GetByIdAsync(Guid fileId, CancellationToken cancellationToken)
    {
        return await context
            .Set<BlobFileInfo>()
            .FirstOrDefaultAsync(file => file.FileId == fileId && !file.IsDeleted, cancellationToken);
    }

    public async Task<BlobFileInfo?> GetByIdFromDeletedAsync(Guid fileId, CancellationToken cancellationToken)
    {
        return await context
            .Set<BlobFileInfo>()
            .FirstOrDefaultAsync(file => file.FileId == fileId && file.IsDeleted, cancellationToken);
    }

    public IAsyncEnumerable<BlobFileInfo> GetUnusedFilesAsync(int count, CancellationToken cancellationToken)
    {
        var dateToFind 
        return context
            .Set<BlobFileInfo>()
            .Where(file => !(file.LastDownloadDate))
            .Take(count)
            .AsAsyncEnumerable();
    }
}