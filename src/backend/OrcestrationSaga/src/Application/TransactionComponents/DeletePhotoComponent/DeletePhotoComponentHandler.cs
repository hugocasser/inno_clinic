using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Result;

namespace Application.TransactionComponents.DeletePhotoComponent;

public class DeletePhotoComponentHandler(IFilesService filesService) : ITransactionComponentHandler
{
    public const string HandlerKey = "a6a5b4f9-5f8a-4b9e-8b9f-5b9f5b9f5b9f";
    public bool RollbackRequired { get; } = false;
    public bool NeedRollback { get; } = true;
    private Guid _photoId = Guid.Empty;
    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithPhotoDeleting transaction)
        {
            return ResultBuilder.TransactionFailed();
        }

        if (transaction.PhotoId == null)
        {
            return ResultBuilder.TransactionSuccess();
        }
        
        var result = await filesService.DeletePhotoAsync(transaction.PhotoId.Value, cancellationToken);
        
        if (!result.IsSuccess)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        _photoId = transaction.PhotoId.Value;
        
        return ResultBuilder.TransactionSuccess();
        
    }

    public async Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        if (_photoId == Guid.Empty)
        {
            return ResultBuilder.TransactionSuccess();
        }
        
        var result = await filesService.TryRollbackAsync(_photoId, cancellationToken);
        
        if (!result.IsSuccess)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        return ResultBuilder.TransactionSuccess();
    }
}