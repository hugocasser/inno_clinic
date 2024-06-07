using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ValidationServices;
using Polly;

namespace Application.Services.ValidationServices.DecoratedServicesWithRateLimiting;

public class DecoratedPhoneValidatorService
    (IPhoneValidatorService phoneValidatorService,
        ResiliencePipeline<IResult> resiliencePipeline)
    : IPhoneValidatorService
{
    public async Task<IResult> ValidatePhoneNumberAsync(string phoneNumber, CancellationToken token = default)
    {
        return await resiliencePipeline
            .ExecuteAsync(async ct =>
                await phoneValidatorService.ValidatePhoneNumberAsync(phoneNumber, ct), token);
    }
}