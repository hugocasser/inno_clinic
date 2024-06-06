using DataAccess.Dtos;

namespace DataAccess.Abstractions;

public interface IBlobService
{
    public Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken);
    public Task<FileResponse> GetByIdAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<bool> IsExistsAsync(Guid fileId, CancellationToken cancellationToken);
}