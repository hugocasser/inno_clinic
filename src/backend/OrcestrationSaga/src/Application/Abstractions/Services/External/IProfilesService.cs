using Application.TransactionComponents.CreateProfileComponent;
using Application.TransactionComponents.UpdateProfileComponent;

namespace Application.Abstractions.Services.External;

public interface IProfilesService
{
    public Task<IResult> CreateProfileAsync(ITransactionWithProfileCreation transaction, CancellationToken cancellationToken);
    public Task<IResult> UpdatePatientsProfileAsync(ITransactionWithProfileUpdating transaction, CancellationToken cancellationToken);
    public Task<IResult> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken);
    public Task<IResult> TryRollbackAccountAsync(Guid doctorId, CancellationToken cancellationToken);
}