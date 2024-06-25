using Dapper;
using DLL.Abstractions.Persistence.Repositories;
using DLL.Models;
using DLL.Options;
using Microsoft.Extensions.Options;

namespace DLL.Persistence.Repositories;

public class SpecializationsRepository(IOptions<PostgresOptions> options) :
    BaseRepository(options), ISpecializationsRepository
{
    public async Task<int> AddAsync(Specialization specialization)
    {
        var query = $"INSERT INTO Specializations (Id, Name, IsActive) VALUES" +
            $" ({specialization.Id}, {specialization.Name}, {specialization.IsActive})";
        
        var result = await DbTransaction.Connection!.ExecuteAsync(query, DbTransaction);
        
        return result;
    }

    public async Task<IReadOnlyCollection<Specialization>> GetAllAsync(int take = 10, int skip = 0)
    {
        var query = $"SELECT * FROM Specializations WHERE IsDeleted = false LIMIT {take} OFFSET {skip}";
        
        var result = await DbTransaction.Connection!
            .QueryAsync<Specialization>(query, DbTransaction);
        
        return result.ToList();
    }

    public Task<int> UpdateStatusAsync(Guid id, bool status)
    {
        var query = $"UPDATE Specializations SET IsActive = {status} WHERE Id = @Id AND IsDeleted = false";
        
        return DbTransaction.Connection!.ExecuteAsync(query, new { Id = id }, DbTransaction);
    }

    public Task<int> UpdateAsync(Specialization specialization, bool status)
    {
        const string query = $"UPDATE Specializations SET Name = @Name, IsActive = @IsActive WHERE Id = @Id AND IsDeleted = false";
        
        return DbTransaction.Connection!.ExecuteAsync(query, new
        {
            Id = specialization.Id,
            Name = specialization.Name,
            IsActive = status
        }, DbTransaction);
    }

    public Task<bool> IsExistsAsync(Guid id)
    {
        const string query = $"SELECT EXISTS(SELECT * FROM Specializations WHERE Id = @Id AND IsDeleted = false)";

        return DbTransaction.Connection!.QuerySingleAsync<bool>(query, new { Id = id }, DbTransaction);
    }

    public Task<Specialization?> GetByIdAsync(Guid id)
    {
        const string query = $"SELECT * FROM Specializations WHERE Id = @Id AND IsDeleted = false";

        return DbTransaction.Connection!.QuerySingleAsync<Specialization?>(query, new { Id = id }, DbTransaction);
    }
}