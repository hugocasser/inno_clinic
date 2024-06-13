namespace Application.TransactionComponents.DeleteProfileComponent;

public interface ITransactionWithProfileDeleting
{
    public Guid ProfileId { get; set; }
    public void SetAccountId(Guid accountId);
    public void SetPhotoId(Guid? photoId);
}