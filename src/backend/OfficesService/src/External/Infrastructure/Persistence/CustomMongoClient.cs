using Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Persistence;

public class CustomMongoClient(IOptions<MongoOptions> options) : MongoClient(options.Value.ConnectionString);