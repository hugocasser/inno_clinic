using Application.Abstractions.Services.External;
using Application.Abstractions.Services.Saga;
using Application.Result;

namespace Application.TransactionComponents.UploadFileComponent;

public class FileUploaderComponentHandler(IFilesService filesService) : ITransactionComponentHandler
{
    public const string HandlerKey = "9ee151a7-98b0-4381-86b9-fb7e0b6dcba7";
    private Guid FileId { get; set; } = Guid.Empty;
    public bool RollbackRequired { get; } = false;
    public bool NeedRollback { get; } = true;
    public async Task<ITransactionResult> HandleAsync(object request, CancellationToken cancellationToken = default)
    {
        if (request is not ITransactionWithFileUploading transaction)
        {
            return ResultBuilder.TransactionFailed();
        }
        
        var uploadResult = await filesService.UploadFileAsync(transaction.Photo, cancellationToken);

        if (!uploadResult.IsSuccess)
        {
            return ResultBuilder.TransactionNoContent();
        }

        FileId = uploadResult.GetContent<Guid>();
        transaction.SetFileId(FileId);
        
        return ResultBuilder.TransactionSuccess();
    }

    public async Task<ITransactionResult> TryRollbackAsync(CancellationToken cancellationToken = default)
    {
        if (FileId == Guid.Empty)
        {
            return ResultBuilder.TransactionFailed();
        }

        var rollbackResult = await filesService.TryRollbackAsync(FileId, cancellationToken);
        
        return !rollbackResult.IsSuccess
            ? ResultBuilder.TransactionFailed()
            : ResultBuilder.TransactionSuccess();
    }
}
    