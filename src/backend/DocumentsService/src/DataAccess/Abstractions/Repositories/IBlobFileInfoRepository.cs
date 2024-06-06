using DataAccess.Models;

namespace DataAccess.Abstractions.Repositories;

public interface IBlobFileInfoRepository
{
    public Task CreateAsync(BlobFileInfo fileInfo, CancellationToken cancellationToken);
    public Task UpdateAsync(BlobFileInfo fileInfo);
    public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<BlobFileInfo?> GetByIdAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<BlobFileInfo?> GetByIdFromDeletedAsync(Guid fileId, CancellationToken cancellationToken);
    public IAsyncEnumerable<BlobFileInfo> GetUnusedFilesAsync(int count,CancellationToken cancellationToken);
}