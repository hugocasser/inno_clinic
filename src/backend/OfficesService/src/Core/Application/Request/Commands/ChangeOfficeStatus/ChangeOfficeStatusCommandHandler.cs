using Application.Abstractions.OperationResult;
using Application.Abstractions.Repositories;
using Application.Dtos;
using Application.OperationResults;
using MediatR;

namespace Application.Request.Commands.ChangeOfficeStatus;

public class ChangeOfficeStatusCommandHandler(IWriteOfficesRepository writeOfficesRepository) : IRequestHandler<ChangeOfficeStatusCommand,IResult>
{
    public async Task<IResult> Handle(ChangeOfficeStatusCommand request, CancellationToken cancellationToken)
    {
        var office = await writeOfficesRepository.GetOfficeAsync(request.OfficeId);
        
        if (office is null)
        {
            return ResultBuilder.NotFound(ErrorMessages.OfficeNotFound);
        }
        
        if (office.IsActive == request.IsActive)
        {
            return ResultBuilder.BadRequest(ErrorMessages.NothingChanged);
        }
        
        office.ChangeOfficeStatus((request).IsActive);
        await writeOfficesRepository.UpdateOfficeAsync(office);
        
        return ResultBuilder.Success().WithStatusCode(200).WithData(OfficeWithoutPhotoViewDto.MapFromModel(office));
    }
}