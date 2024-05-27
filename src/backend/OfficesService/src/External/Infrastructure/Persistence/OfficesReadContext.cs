using Domain.Models;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class OfficesReadContext
{
    public IMongoCollection<Office> Offices { get; set; }
    
    public OfficesReadContext(IOptions<MongoOptions> options, IMongoClient client)
    {
        var database = client.GetDatabase(options.Value.DatabaseName);
        Offices = database.GetCollection<Office>(options.Value.CollectionsNames.First());
    }
}