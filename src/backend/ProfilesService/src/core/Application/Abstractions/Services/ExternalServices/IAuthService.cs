using Application.Abstractions.OperationResult;
using Application.OperationResult.Results;

namespace Application.Abstractions.Services.ExternalServices;

public interface IAuthService
{
    public Task<OperationResult<Guid>> RegisterDoctorAsync(string email, CancellationToken cancellationToken);
    public Task<OperationResult<Guid>> RegisterPatientAsync(string email, CancellationToken cancellationToken);
    public Task<OperationResult<Guid>> RegisterReceptionistAsync(string email, CancellationToken cancellationToken);
}