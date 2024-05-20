using Application.Abstractions.Cache;
using StackExchange.Redis;

namespace Application.Services.Cache;

public class CacheService(IConnectionMultiplexer connectionMultiplexer) : ICacheService
{
    
    private readonly IDatabase _database = connectionMultiplexer.GetDatabase();
    
    public Task SetAsync(string key, string serializedValue, TimeSpan? expiration = null,
        CancellationToken cancellationToken = default)
    {
        return _database.StringSetAsync(key, serializedValue, expiration);
    }

    public async Task<string?> GetAsync(string key, CancellationToken cancellationToken = default)
    {
        var token = await _database.StringGetAsync(key);     
        
        return token.HasValue ? token.ToString() : null;
    }

    public Task RemoveAsync(string key, CancellationToken cancellationToken = default)
    {
        return _database.KeyDeleteAsync(key);
    }
}