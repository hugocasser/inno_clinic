using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Dtos;
using Application.Dtos.View;
using Application.OperationResults;
using MediatR;

namespace Application.Request.Commands.ChangeOfficePhoto;

public class ChangeOfficePhotoCommandHandler(IWriteOfficesRepository writeOfficesRepository) : IRequestHandler<ChangeOfficePhotoCommand, IResult>
{
    public async Task<IResult> Handle(ChangeOfficePhotoCommand request, CancellationToken cancellationToken)
    {
        var office = await writeOfficesRepository.GetOfficeAsync(request.OfficeId, cancellationToken);
        
        if (office is null)
        {
            return ResultBuilder.NotFound(ErrorMessages.OfficeNotFound);
        }

        if (office.PhotoId == request.PhotoId)
        {
            return ResultBuilder.BadRequest(ErrorMessages.NothingChanged);
        }
        
        office.ChangeOfficePhoto(request.PhotoId);
        await writeOfficesRepository.UpdateOfficeAsync(office, cancellationToken);
        
        return ResultBuilder.Success().WithStatusCode(200).WithData(OfficeWithoutPhotoViewDto.MapFromModel(office));
    }
}