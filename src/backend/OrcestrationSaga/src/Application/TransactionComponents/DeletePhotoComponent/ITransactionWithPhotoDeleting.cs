namespace Application.TransactionComponents.DeletePhotoComponent;

public interface ITransactionWithPhotoDeleting
{
    public Guid? PhotoId { get; set; }
}