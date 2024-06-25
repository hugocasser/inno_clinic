using System.Data;
using DLL.Abstractions.Persistence.Repositories;
using DLL.Options;
using Microsoft.Extensions.Options;
using Npgsql;

namespace DLL.Persistence.Repositories;

public class BaseRepository : IBaseRepository
{
    protected readonly IDbTransaction DbTransaction;

    protected BaseRepository(IOptions<PostgresOptions> options)
    {
        var dbConnection = new NpgsqlConnection(options.Value.ConnectionString);
        dbConnection.Open();
        DbTransaction = dbConnection.BeginTransaction();
    }

    public IDbTransaction CurrentTransaction()
    {
        return DbTransaction;
    }

    public void SaveChanges()
    {
        DbTransaction.Commit();
        DbTransaction.Connection!.Close();
    }
}