using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Read;
using Application.Abstractions.Repositories.Write;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.RollBackReceptionist;

public class RollBackReceptionistProfileCommandHandler
    (IReadReceptionistsRepository readReceptionistsRepository,
     IWriteReceptionistsRepository writeReceptionistsRepository)
    : IRequestHandler<RollBackReceptionistProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(RollBackReceptionistProfileCommand request, CancellationToken cancellationToken = default)
    {
        var receptionist = await writeReceptionistsRepository.GetByIdFromDeletedAsync(request.ReceptionistId, cancellationToken);
        
        if (receptionist is null)
        {
            return HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound);
        }
        
        receptionist.IsDeleted = false;
        
        var readModel = await readReceptionistsRepository.GetByIdFromDeletedAsync(receptionist.Id, cancellationToken);
        
        if (readModel is null)
        {
            receptionist.Created();
        }
        else
        {
            readModel.IsDeleted = false;
            await readReceptionistsRepository.UpdateAsync(readModel, cancellationToken);
        }
        
        await writeReceptionistsRepository.UpdateAsync(receptionist);
        await writeReceptionistsRepository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.NoContent();
    }
    
}