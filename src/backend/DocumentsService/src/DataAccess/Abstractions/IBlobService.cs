using DataAccess.Dtos;
using DataAccess.OperationResult.Abstractions;

namespace DataAccess.Abstractions;

public interface IBlobService
{
    public Task<IResult> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken);
    public Task<IResult> GetByIdAsync(Guid fileId, CancellationToken cancellationToken);
    public Task<bool> IsExistsAsync(Guid fileId, CancellationToken cancellationToken);
    public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken);
}