using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ValidationServices;
using Google.Type;
using Polly;

namespace Application.Services.ValidationServices.DecoratedServicesWithRateLimiting;

public class DecoratedGoogleMapsApiClient
    (IGoogleMapsApiClient googleMapsApiClient,
        ResiliencePipeline<IResult> resiliencePipeline)
    : IGoogleMapsApiClient
{
    public async Task<IResult> ValidateAddressAsync(PostalAddress address, CancellationToken token = default)
    {
        return await resiliencePipeline
            .ExecuteAsync(async ct =>
                await googleMapsApiClient.ValidateAddressAsync(address, ct), token);
    }
}