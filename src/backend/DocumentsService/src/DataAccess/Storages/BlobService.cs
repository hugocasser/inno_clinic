using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DataAccess.Abstractions;
using DataAccess.Dtos;
using DataAccess.Options;
using Microsoft.Extensions.Options;

namespace DataAccess.Storages;

public class BlobService(BlobServiceClient serviceClient, IOptions<BlobOptions> options)
    : IBlobService
{
    private readonly BlobContainerClient _containerClient = serviceClient.GetBlobContainerClient(options.Value.ContainerName);

    public async Task<Guid> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken)
    {
        var fileId = Guid.NewGuid();
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());
        
        await blobClient.UploadAsync(stream, new BlobHttpHeaders
        {
            ContentType = contentType,
            CacheControl = "no-cache"
        }, cancellationToken: cancellationToken);
        
        return fileId;
    }

    public async Task<FileResponse> GetByIdAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());

        var response = await blobClient.DownloadStreamingAsync(cancellationToken:cancellationToken);
        
        return new FileResponse(response.Value.Content, response.Value.Details.ContentType);
    }

    public async Task<bool> IsExistsAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());
        var response = await blobClient.ExistsAsync(cancellationToken: cancellationToken);
        
        return response.Value;
    }
}