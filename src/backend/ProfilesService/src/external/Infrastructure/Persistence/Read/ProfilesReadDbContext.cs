using Application.Abstractions.TransactionalOutbox;
using Application.ReadModels;
using Domain.Abstractions;
using Domain.Models;
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


    public IMongoCollection<TReadModel> Collection<TReadModel, TModel>()
        where TReadModel : class, IReadProfileModel<TModel>
        where TModel : Profile
    {
        return _database.GetCollection<TReadModel>(typeof(TModel).Name);
    }
}