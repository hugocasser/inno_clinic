using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ValidationServices;
using Application.OperationResults;
using Google.Type;

namespace IntegrationTests.Mocks;

public class GoogleMapsApiClientMock : IGoogleMapsApiClient
{
    public Task<IResult> ValidateAddressAsync(PostalAddress? address, CancellationToken token = default)
    { 
        if (address.Locality == "error")
        {
            return Task.FromResult(ResultBuilder.Failure());
        }
        
        return Task.FromResult(ResultBuilder.Success());
    }
}