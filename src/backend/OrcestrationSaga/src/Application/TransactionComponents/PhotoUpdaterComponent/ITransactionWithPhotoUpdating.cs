using Microsoft.AspNetCore.Http;

namespace Application.TransactionComponents.PhotoUpdaterComponent;

public interface ITransactionWithPhotoUpdating
{
    public Guid? PhotoId { get; }
    public IFormFile? Photo { get; }
    public void SetPhotoId(Guid? photoId);
}