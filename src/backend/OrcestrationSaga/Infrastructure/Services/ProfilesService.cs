using Application.Abstractions;
using Application.Abstractions.Services.External;
using Application.Result;
using Application.TransactionComponents.CreateProfileComponent;
using Application.TransactionComponents.UpdateProfileComponent;

namespace Infrastructure.Services;

public class ProfilesService : IProfilesService
{
    
    //TODO: add logic to create profile
    // now it's just mock while services communication not implemented
    public Task<IResult> CreateProfileAsync(ITransactionWithProfileCreation transaction, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }

    public Task<IResult> UpdatePatientsProfileAsync(ITransactionWithProfileUpdating transaction, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }
    

    public Task<IResult> DeleteProfileAsync(Guid profileId, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }

    public Task<IResult> TryRollbackAccountAsync(Guid doctorId, CancellationToken cancellationToken)
    {
        return Task.FromResult(ResultBuilder.Success(""));
    }
}