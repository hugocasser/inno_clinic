using Dapper;
using DLL.Abstractions.Persistence.Repositories;
using DLL.Models;
using DLL.Options;
using Microsoft.Extensions.Options;

namespace DLL.Persistence.Repositories;

public class CategoriesRepository(IOptions<PostgresOptions> options) :
    BaseRepository(options), ICategoriesRepository
{
    public async Task<bool> AddAsync(Category category, CancellationToken cancellationToken)
    {
        const string query = $"INSERT INTO Categories (Id, Name) VALUES (@Id, @Name)";

        var result = await DbTransaction.Connection!
            .ExecuteAsync(query, new { Id = category.Id, Name = category.Name }, DbTransaction);

        return result == 0;
    }

    public async Task<IReadOnlyCollection<Category>> GetAllAsync()
    {
        const string query = $"SELECT Id, Name FROM Categories WHERE IsDeleted = false";

        var result = await DbTransaction.Connection!.QueryAsync<Category>(query, transaction: DbTransaction);

        return result.ToList();
    }

    public async Task<bool> IsExistsAsync(Guid requestCategoryId)
    {
        const string query = $"SELECT Id FROM Categories WHERE Id = @Id AND IsDeleted = false";
        
        var result = await DbTransaction.Connection!.QueryFirstOrDefaultAsync<Category>(query, new
        {
            Id = requestCategoryId
        }, DbTransaction);
        
        return result is not null;
    }
}