using Application.Abstractions.Services.Saga;
using Application.TransactionComponents.CreateProfileComponent;

namespace Application.Abstractions.Services.External;

public interface IProfilesService
{
    Task<IResult> CreateProfileAsync(ITransactionWithProfileCreation transaction, CancellationToken cancellationToken);
    Task<IResult> UpdatePatientsProfileAsync(CancellationToken cancellationToken);
    Task<IResult> UpdateDoctorsProfileAsync(CancellationToken cancellationToken);
    Task<IResult> TryRollbackAccountAsync(Guid doctorId, CancellationToken cancellationToken);
}