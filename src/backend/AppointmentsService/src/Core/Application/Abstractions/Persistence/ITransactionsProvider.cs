namespace Application.Abstractions.Persistence;

public interface ITransactionsProvider
{
    public void StartTransaction();
    public void Commit();
    public void Rollback();
}