using Application.Abstractions.OperationResult;
using Application.Abstractions.Services.ValidationServices;
using Application.OperationResults;

namespace IntegrationTests.Mocks;

public class PhoneValidatorServiceMock : IPhoneValidatorService
{
    public Task<IResult> ValidatePhoneNumberAsync(string phoneNumber, CancellationToken token = default)
    {
        if (phoneNumber == "error")
        {
            return Task.FromResult(ResultBuilder.Failure());
        }
        
        return Task.FromResult(ResultBuilder.Success());
    }
}