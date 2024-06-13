using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Result;

namespace Application.TransactionComponents.PhotoUpdaterComponent;

public class PhotoUpdaterComponentHandler(IFilesService filesService) : ITransactionComponentHandler
{
    public const string HandlerKey = "5a5cfbb4-3fe4-4a3a-a8c7-1d82cc36a6ee";
    public bool RollbackRequired { get; } = false;
    public bool NeedRollback { get; } = false;
    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithPhotoUpdating transaction)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        if (transaction.PhotoId == null)
        {
            var uploadResult = await filesService.UploadFileAsync(transaction.Photo, cancellationToken);

            if (!uploadResult.IsSuccess)
            {
                return ResultBuilder.TransactionFailed();
            }
            
            transaction.SetPhotoId(uploadResult.GetContent<Guid>());
            
            return ResultBuilder.TransactionSuccess();
        }
        
        var result = await filesService.ReplaceFileAsync(transaction.Photo, transaction.PhotoId.Value, cancellationToken);
        
        return !result.IsSuccess 
            ? ResultBuilder.TransactionFailed() 
            : ResultBuilder.TransactionSuccess();
    }

    public Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        return Task.FromResult(ResultBuilder.TransactionSuccess());
    }
}