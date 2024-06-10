using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;
using DataAccess.Abstractions;
using DataAccess.Dtos;
using DataAccess.OperationResult;
using DataAccess.OperationResult.Abstractions;
using DataAccess.OperationResult.Errors;
using DataAccess.Options;
using Microsoft.Extensions.Options;

namespace DataAccess.Storages;

public class BlobService(BlobServiceClient serviceClient, IOptions<BlobOptions> options)
    : IBlobService
{
    private readonly BlobContainerClient _containerClient = serviceClient.GetBlobContainerClient(options.Value.ContainerName);

    public async Task<IResult> UploadAsync(Stream stream, string contentType, CancellationToken cancellationToken)
    {
        var fileId = Guid.NewGuid();
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());
        
        var response = await blobClient.UploadAsync(stream, new BlobHttpHeaders
        {
            ContentType = contentType,
            CacheControl = "no-cache"
        }, cancellationToken: cancellationToken);
        
        var rawResponse = response.GetRawResponse();
        
        return rawResponse.IsError
            ? ResultBuilder.Failure(new Error(rawResponse.ReasonPhrase))
            : ResultBuilder.Success(fileId);
    }

    public async Task<IResult> GetByIdAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());

        var response = await blobClient.DownloadStreamingAsync(cancellationToken:cancellationToken);
        
        var rawResponse = response.GetRawResponse();
        
        return rawResponse.IsError ?
            ResultBuilder.Failure(new Error(rawResponse.ReasonPhrase))
            : ResultBuilder.Success(new FileResponse(response.Value.Content, response.Value.Details.ContentType));
    }

    public async Task<bool> IsExistsAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());
        var response = await blobClient.ExistsAsync(cancellationToken: cancellationToken);
        
        return response.Value;
    }

    public Task DeleteAsync(Guid fileId, CancellationToken cancellationToken)
    {
        var blobClient = _containerClient.GetBlobClient(fileId.ToString());
        
        return blobClient.DeleteIfExistsAsync(cancellationToken: cancellationToken);
    }
}