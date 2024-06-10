using Microsoft.AspNetCore.Http;

namespace Application.TransactionComponents.UploadFileComponent;

public interface ITransactionWithFileUploading
{
    public IFormFile Photo { get; set; }
    public Guid FileId { get; set; }
    public void SetFileId(Guid fileId);
}