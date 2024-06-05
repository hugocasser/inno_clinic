using Application.Abstractions.TransactionalOutbox;
using Infrastructure.Options;
using Infrastructure.Services;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Read;

public class ProfilesReadDbContext
{
    private readonly IMongoDatabase _database;

    public ProfilesReadDbContext(IOptions<MongoOptions> options)
    {
        var client = new CustomMongoClient(options);
        
        _database = client.GetDatabase(options.Value.DatabaseName);
    }


    public IMongoCollection<TReadModel> Collection<TReadModel>()
        where TReadModel : class, IReadProfileModel
    {
        return _database.GetCollection<TReadModel>(typeof(TReadModel).Name);
    }
}