using Microsoft.AspNetCore.Http;

namespace Application.TransactionComponents.UploadFileComponent;

public interface ITransactionWithFileUploading
{
    public IFormFile? File { get; set; }
    public Guid FileId { get; set; }
    public void SetFileId(Guid fileId);
}