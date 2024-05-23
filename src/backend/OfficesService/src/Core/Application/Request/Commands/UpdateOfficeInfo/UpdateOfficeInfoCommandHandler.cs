using Application.Abstractions.OperationResult;
using Application.Abstractions.Repositories;
using Application.Dtos;
using Application.OperationResults;
using MediatR;

namespace Application.Request.Commands.UpdateOfficeInfo;

public class UpdateOfficeInfoCommandHandler
    (IWriteOfficesRepository writeOfficesRepository)
    : IRequestHandler<UpdateOfficeInfoCommand, IResult>
{
    public async Task<IResult> Handle(UpdateOfficeInfoCommand request, CancellationToken cancellationToken)
    {
        var office = await writeOfficesRepository.GetOfficeAsync(request.OfficeId);
        
        if (office is null)
        {
            return ResultBuilder.NotFound(ErrorMessages.OfficeNotFound);
        }

        if (office.RegistryPhoneNumber == request.RegistryPhoneNumber && office.Address == request.Address)
        {
            return ResultBuilder.BadRequest(ErrorMessages.NothingChanged);
        }
        
        office.UpdateOffice(request.OfficeId, request.Address, request.RegistryPhoneNumber );
        await writeOfficesRepository.UpdateOfficeAsync(office);
        
        var officeViewDto = OfficeWithoutPhotoViewDto.MapFromModel(office);
        
        return ResultBuilder.Success().WithData(officeViewDto).WithStatusCode(200);
    }
}