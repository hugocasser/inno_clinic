using Application.Abstractions.CQRS;
using Application.Abstractions.Repositories.Write;
using Application.Dtos.Views.Receptionists;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.CreateReceptionistProfile;

public class CreateReceptionistProfileCommandHandler
    (IWriteReceptionistsRepository receptionistsRepository)
    : IRequestHandler<CreateReceptionistProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(CreateReceptionistProfileCommand request, CancellationToken cancellationToken = default)
    {
        var receptionist = request.MapToModel();
        
        receptionist.Created();
        
        await receptionistsRepository.AddAsync(receptionist, cancellationToken);
        await receptionistsRepository.SaveChangesAsync(cancellationToken);
        
        return HttpResultBuilder.Success(ReceptionistViewDto.MapFromModel(receptionist));
    }
}