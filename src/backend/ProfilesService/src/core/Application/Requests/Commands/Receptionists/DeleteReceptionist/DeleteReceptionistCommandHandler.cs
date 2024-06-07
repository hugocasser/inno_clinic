using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.OperationResult;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.DeleteReceptionist;

public class DeleteReceptionistCommandHandler
    (IWriteReceptionistsRepository receptionistsRepository)
    : IRequestHandler<DeleteReceptionistCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(DeleteReceptionistCommand request, CancellationToken cancellationToken = default)
    {
        var receptionist = await receptionistsRepository.GetByIdAsync(request.ReceptionistId, cancellationToken);
        
        if (receptionist is null)
        {
            return HttpResultBuilder.NotFound(HttpErrorMessages.ProfileNotFound);
        }
        
        receptionist.Deleted();
        
        await receptionistsRepository.DeleteAsync(receptionist.Id, cancellationToken);
        await receptionistsRepository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.NoContent();
    }
}