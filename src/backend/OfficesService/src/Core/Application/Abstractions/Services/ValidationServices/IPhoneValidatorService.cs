using Application.Abstractions.OperationResult;

namespace Application.Abstractions.Services.ValidationServices;

public interface IPhoneValidatorService
{
    public Task<IResult> ValidatePhoneNumberAsync(string phoneNumber, CancellationToken token = default);
}