namespace Application.Abstractions.Services.ValidationServices;

public interface IGoogleMapsApiClient
{
    public Task<bool> ValidateAddressAsync(string address, CancellationToken token = default);
}