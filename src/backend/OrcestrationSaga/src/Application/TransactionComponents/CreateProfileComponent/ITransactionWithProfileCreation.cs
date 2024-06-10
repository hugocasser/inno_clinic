namespace Application.TransactionComponents.CreateProfileComponent;

public interface ITransactionWithProfileCreation
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string MiddleName { get; set; }
    public Guid OfficeId { get; set; }
    public Guid AccountId { get; set; }
}