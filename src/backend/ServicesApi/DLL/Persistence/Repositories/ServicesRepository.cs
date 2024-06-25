using System.Data;
using Dapper;
using DLL.Abstractions.Persistence.Repositories;
using DLL.Models;
using DLL.Options;
using Microsoft.Extensions.Options;

namespace DLL.Persistence.Repositories;

public class ServicesRepository(IOptions<PostgresOptions> options) :
    BaseRepository(options), IServicesRepository
{
    public Task<int> AddAsync(Service service)
    {
        const string query = $"INSERT INTO Services" +
            $" (Id, Name, SpecializationId, CategoryId, IsActive, Price, IsDeleted)" +
            $" VALUES (@Id, @Name, @SpecializationId, @CategoryId, @IsActive, @Price, @IsDeleted)";
        
        return DbTransaction.Connection!.ExecuteAsync(query, new
        {
            Id = service.Id,
            Name = service.Name,
            SpecializationId = service.SpecializationId,
            CategoryId = service.CategoryId,
            IsActive = service.IsActive,
            Price = service.Price,
            IsDeleted = false
        }, DbTransaction);
    }

    public Task<IEnumerable<Service>> GetAllAsync(int take = 10, int skip = 0)
    {
        const string query = $"SELECT Id, Name, IsActive, Price FROM Services WHERE IsDeleted = false LIMIT @Take OFFSET @Skip";
        
        return DbTransaction.Connection!.QueryAsync<Service>(query, new 
            {
                Take =take,
                Skip = skip
            },  DbTransaction);
    }

    public Task<int> UpdateStatusAsync(Guid id, bool status)
    {
        const string query = $"UPDATE Services SET IsActive = @Status WHERE Id = @Id AND IsDeleted = false";
        
        return DbTransaction.Connection!.ExecuteAsync(query, new
        {
            Id = id,
            Status = status
        }, DbTransaction);
    }

    public Task<int> UpdateAsync(Service service)
    {
        const string query = $"UPDATE Services SET Name = @Name, IsActive = @Status WHERE Id = @Id AND IsDeleted = false";
        
        return DbTransaction.Connection!.ExecuteAsync(query, new
        {
            Id = service.Id,
            Name = service.Name,
            IsActive= service.IsActive
        }, DbTransaction);
    }

    public async Task<bool> IsExistsAsync(Guid id)
    {
        const string query = $"SELECT Id FROM Services WHERE Id = @Id AND IsDeleted = false";
        
        var result = await DbTransaction.Connection!
            .QueryFirstOrDefaultAsync<Service>(query, new { Id = id }, DbTransaction);
        
        return result is not null;
    }

    public Task<Service?> GetByIdAsync(Guid id)
    {
        const string query = $"SELECT * FROM Services WHERE Id = @Id AND IsDeleted = false";
        
        return DbTransaction.Connection!
            .QueryFirstOrDefaultAsync<Service>(query, new { Id = id }, DbTransaction);
    }

    public Task<int> DeleteAsync(Guid serviceId)
    {
        const string query = $"UPDATE Services SET IsDeleted = true WHERE Id = @Id";
        
        return DbTransaction.Connection!.ExecuteAsync(query, new
        {
            Id = serviceId
        }, DbTransaction);
    }
}