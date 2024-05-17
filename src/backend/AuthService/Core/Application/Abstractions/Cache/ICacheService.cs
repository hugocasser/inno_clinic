namespace Application.Abstractions.Cache;

public interface ICacheService
{
    public Task SetAsync(string key, string serializedValue, TimeSpan? expiration = null, CancellationToken cancellationToken = default);
    public Task<string?> GetAsync(string key, CancellationToken cancellationToken = default);
    public Task RemoveAsync(string key, CancellationToken cancellationToken = default);
}