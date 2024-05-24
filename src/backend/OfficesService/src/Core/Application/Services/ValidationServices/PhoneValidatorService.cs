using System.Net.Http.Json;
using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ValidationServices;
using Application.Dtos.Validation.PhoneValidation;
using Application.OperationResults;
using Application.Options;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Application.Services.ValidationServices;

public class PhoneValidatorService
    (ILogger<PhoneValidatorService> logger,
        IHttpClientFactory httpClientFactory,
        IOptions<PhoneValidatorOptions> options) : IPhoneValidatorService
{
    public async Task<IResult> ValidatePhoneNumberAsync(string phoneNumber, CancellationToken token = default)
    {
        using var httpClient = httpClientFactory.CreateClient();
        var requestUrl = ConfigureRequest(phoneNumber);

        try
        {
            var response = await httpClient.GetAsync(requestUrl, token);

            if (!response.IsSuccessStatusCode)
            {
                return ResultBuilder.Failure().WithData("Failed to validate phone number, status code: " + response.StatusCode);
            }
            
            var result = await response.Content
                .ReadFromJsonAsync<PhoneValidatorResponse>(cancellationToken: token);

            return result is { Valid: true } 
                ? ResultBuilder.Success() 
                : ResultBuilder.Failure().WithData("Phone number is not valid");
        }
        catch (Exception exception)
        {
            logger.LogError(exception, "Failed to validate phone number, {PhoneNumber}, cause: phone api error", phoneNumber);
            
            return ResultBuilder.Failure().WithData("Failed to validate phone number").WithStatusCode(500);
        }
    }
    
    private string ConfigureRequest(string phoneNumber)
    {
        return $"https://phonevalidation.abstractapi.com/v1/?api_key={options.Value.ApiKey}&phone={phoneNumber}";
    }
}

