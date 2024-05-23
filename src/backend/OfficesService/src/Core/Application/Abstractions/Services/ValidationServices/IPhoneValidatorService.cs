namespace Application.Abstractions.Services.ValidationServices;

public interface IPhoneValidatorService
{
    public Task<bool> ValidatePhoneNumberAsync(string phoneNumber, CancellationToken token = default);
}