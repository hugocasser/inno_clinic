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
        var query = $"INSERT INTO Categories (Id, Name) VALUES ({category.Id}, {category.Name})";

        var result = await DbTransaction.Connection!
            .ExecuteAsync(query, new { Id = category.Id, Name = category.Name }, DbTransaction);

        return result == 0;
    }

    public async Task<IReadOnlyCollection<Category>> GetAllAsync(int take = 10, int skip = 0,
        CancellationToken cancellationToken = default)
    {
        var query = $"SELECT * FROM Categories WHERE IsDeleted = false LIMIT {take} OFFSET {skip}";

        var result = await DbTransaction.Connection!.QueryAsync<Category>(query, transaction: DbTransaction);

        return result.ToList();
    }
}