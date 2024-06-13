namespace Application.TransactionComponents.DeleteAccountComponent;

public interface ITransactionWithAccountDeleting
{
    public Guid AccountId { get; set; }
}