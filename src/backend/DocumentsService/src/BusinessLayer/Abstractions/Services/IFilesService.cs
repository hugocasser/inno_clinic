using DataAccess.Dtos;
using DataAccess.OperationResult.Abstractions;

namespace BusinessLayer.Abstractions.Services;

public interface IFilesService
{
    public Task<IResult> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken);
    public Task<IResult> GetByIdAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<IResult> RollBackUploadAsync(Guid fileId, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<bool> IsExistsAsync(Guid fileId, CancellationToken cancellationToken);
}