using DataAccess.Models;

namespace DataAccess.Abstractions.Repositories;

public interface IBlobFileInfoRepository
{
    public Task CreateAsync(BlobFileInfo fileInfo, CancellationToken cancellationToken);
    public Task UpdateAsync(BlobFileInfo fileInfo);
    public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<bool> IsExistsAsync(Guid fileId, CancellationToken cancellationToken);
    public Task DeleteFromBoothStoragesAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<BlobFileInfo?> GetByIdAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<BlobFileInfo?> GetByIdFromDeletedAsync(Guid fileId, CancellationToken cancellationToken);
    public IAsyncEnumerable<BlobFileInfo> GetUnusedFilesAsync(int count, CancellationToken cancellationToken = default);
    public Task SaveChangesAsync(CancellationToken cancellationToken);
}