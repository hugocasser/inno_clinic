namespace Application.TransactionComponents.UpdateProfileComponent;

public interface ITransactionWithProfileUpdating
{
    public Guid ProfileId { get; set; }
    public void SetPhotoId(Guid? photoId);
}