using Domain.Models;
using MongoDB.Driver;

namespace Infrastructure.Abstractions;

public interface IOfficesReadDbContext
{
    public IMongoCollection<Office> Offices { get; }
}