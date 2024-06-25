using System.Data;

namespace DLL.Abstractions.Persistence.Repositories;

public interface IBaseRepository
{
    public IDbTransaction CurrentTransaction();
    public void Commit();
    public void Rollback();
}