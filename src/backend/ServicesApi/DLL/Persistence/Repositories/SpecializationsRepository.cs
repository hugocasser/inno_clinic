using Dapper;
using DLL.Abstractions.Persistence.Repositories;
using DLL.Models;
using DLL.Options;
using Microsoft.Extensions.Options;

namespace DLL.Persistence.Repositories;

public class SpecializationsRepository(IOptions<PostgresOptions> options) :
    BaseRepository(options), ISpecializationsRepository
{
    public Task<int> AddAsync(Specialization specialization)
    {
        const string query = $"INSERT INTO Specializations (Id, Name, IsActive, IsDeleted) VALUES (@Id, @Name, @IsActive, false)";
        
        return DbTransaction.Connection!.ExecuteAsync(query,new
        {
            Id = specialization.Id,
            Name = specialization.Name,
            IsActive = specialization.IsActive
        }, DbTransaction);
    }

    public Task<IEnumerable<Specialization>> GetAllAsync(int take = 10, int skip = 0)
    {
        const string query = $"SELECT * FROM Specializations WHERE IsDeleted = false LIMIT @Take OFFSET @Take";
        
        return DbTransaction.Connection!
            .QueryAsync<Specialization>(query,new
            {
                Take = take,
                Skip = skip
            }, DbTransaction);
    }

    public Task<int> UpdateStatusAsync(Guid id, bool status)
    {
        const string query = $"UPDATE Specializations SET IsActive = @Status WHERE Id = @Id AND IsDeleted = false";

        return DbTransaction.Connection!.ExecuteAsync(query, new
        {
            Id = id,
            IsActive = status
        }, DbTransaction);
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