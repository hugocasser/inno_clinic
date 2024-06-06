using DataAccess.Dtos;

namespace BusinessLayer.Abstractions.Services;

public interface IPhotosService
{
    public Task UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken);
    public Task RollBackUploadAsync(Guid fileId, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<bool> IsExistsAsync(Guid fileId, CancellationToken cancellationToken);
}