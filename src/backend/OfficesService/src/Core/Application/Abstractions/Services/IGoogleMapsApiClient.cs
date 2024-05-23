namespace Application.Abstractions.Services;

public interface IGoogleMapsApiClient
{
    public Task<bool> ValidateAddressAsync(string address, CancellationToken token = default);
}