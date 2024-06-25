using System.Data;
using Dapper;

namespace DLL.Persistence.Repositories;

public static class RepositoriesExtensions
{
    public static Task<T?> GetByIdAsync<T>(this IDbConnection dbConnection, Guid id, IDbTransaction? transaction = null)
    {
        var query = $"SELECT * FROM {typeof(T).Name} WHERE Id = @Id";
        
        return dbConnection.QuerySingleAsync<T?>(query, new {Id = id}, transaction);
    }

    public static Task<bool> IsExistsAsync<T>(this IDbConnection dbConnection, Guid id, IDbTransaction? transaction = null)
    {
        var query = $"SELECT EXISTS(SELECT * FROM {typeof(T).Name} WHERE Id = @Id AND IsDeleted = false)";
        
        return dbConnection.QuerySingleAsync<bool>(query, new { Id = id }, transaction);
    }

    public static Task DeleteAsync<T>(this IDbConnection dbConnection, Guid id, IDbTransaction? transaction = null)
    {
        return dbConnection.ExecuteAsync($"UPDATE {typeof(T).Name} SET IsDeleted = true WHERE Id = @Id", new { Id = id }, transaction);
    }
}