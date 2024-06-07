using Application.Abstractions.OperationResult;
using Google.Type;

namespace Application.Abstractions.Services.ValidationServices;

public interface IGoogleMapsApiClient
{
    public Task<IResult> ValidateAddressAsync(PostalAddress address, CancellationToken token = default);
}