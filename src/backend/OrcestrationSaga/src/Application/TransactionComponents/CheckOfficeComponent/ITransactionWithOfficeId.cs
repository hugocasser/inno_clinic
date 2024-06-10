namespace Application.TransactionComponents.CheckOfficeComponent;

public interface ITransactionWithOfficeId
{
    public Guid OfficeId { get; set; }
}