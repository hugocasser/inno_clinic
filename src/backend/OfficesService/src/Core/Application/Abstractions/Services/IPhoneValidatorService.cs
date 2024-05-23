namespace Application.Abstractions.Services;

public interface IPhoneValidatorService
{
    public Task<bool> ValidatePhoneNumberAsync(string phoneNumber, CancellationToken token = default);
}