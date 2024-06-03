using Application.Abstractions.OperationResult;
using Application.Abstractions.Services;
using Application.Abstractions.Services.ExternalServices;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Services.External;

public class AuthService(IPasswordGeneratorService passwordGeneratorService) : IAuthService
{
    // TODO: implement auth service
    // now it's just mock while i don't implement services communication
    public Task<OperationResult<Guid>> RegisterDoctorAsync(string email, CancellationToken cancellationToken)
    {
        return Task.FromResult(new OperationResult<Guid>(Guid.NewGuid()));
    }

    public Task<OperationResult<Guid>> RegisterPatientAsync(string email, CancellationToken cancellationToken)
    {
        return Task.FromResult(new OperationResult<Guid>(Guid.NewGuid()));
    }

    public Task<OperationResult<Guid>> RegisterReceptionistAsync(string email, CancellationToken cancellationToken)
    {
        return Task.FromResult(new OperationResult<Guid>(Guid.NewGuid()));
    }

    public Task<OperationResult<bool>> DeleteAccountAsync(Guid patientUserId, CancellationToken cancellationToken)
    {
        return Task.FromResult(OperationResultBuilder.Success());
    }

    public Task<OperationResult<bool>> UpdatePhoneNumberAsync(Guid userId, string phoneNumber, CancellationToken cancellationToken)
    {
        return Task.FromResult(OperationResultBuilder.Success());
    }
}