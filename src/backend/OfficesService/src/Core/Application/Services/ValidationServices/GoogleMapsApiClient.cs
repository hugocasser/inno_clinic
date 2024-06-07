using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ValidationServices;
using Application.OperationResults;
using Application.Options;
using Google.Api.Gax;
using Google.Api.Gax.Grpc;
using Google.Maps.AddressValidation.V1;
using Google.Type;
using Grpc.Core;
using Microsoft.Extensions.Options;

namespace Application.Services.ValidationServices;
 
public class GoogleMapsApiClient(IOptions<GoogleApiClientOptions> googleApiClientOptions) : IGoogleMapsApiClient
{
    private readonly GoogleApiClientOptions _googleApiClientOptions = googleApiClientOptions.Value;
    public async Task<IResult> ValidateAddressAsync(PostalAddress address, CancellationToken token = default)
    {
        var googleMapsClient = await AddressValidationClient.CreateAsync(token);

        var request = new ValidateAddressRequest()
        {
            Address = address,
            EnableUspsCass = true,
            SessionToken = "",
            PreviousResponseId = "",
        };
        
        var callSetting = CallSettings
            .FromExpiration(Expiration.FromTimeout(TimeSpan.FromSeconds(_googleApiClientOptions.ExpirationInSeconds)))
            .WithCancellationToken(token)
            .WithRetry(RetrySettings.
                FromConstantBackoff(_googleApiClientOptions.MaxAttempts, TimeSpan.FromSeconds(_googleApiClientOptions.BackoffInSeconds),
                    RetrySettings.FilterForStatusCodes(_googleApiClientOptions.FilterForStatusCodes)));
        
        var validationResult =await googleMapsClient.ValidateAddressAsync(request, callSetting);

        if (validationResult is not null && validationResult.Result.Verdict.AddressComplete)
        {
            ResultBuilder.Failure();
        }
        
        
        return ResultBuilder.Success();
    }
}