using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Dtos;
using Application.Dtos.View;
using Application.OperationResults;
using MediatR;

namespace Application.Request.Commands.UpdateOfficeInfo;

public class UpdateOfficeInfoCommandHandler
    (IWriteOfficesRepository writeOfficesRepository)
    : IRequestHandler<UpdateOfficeInfoCommand, IResult>
{
    public async Task<IResult> Handle(UpdateOfficeInfoCommand request, CancellationToken cancellationToken)
    {
        var office = await writeOfficesRepository.GetOfficeAsync(request.OfficeId, cancellationToken);
        
        if (office is null)
        {
            return ResultBuilder.NotFound(ErrorMessages.OfficeNotFound);
        }
        
        var stringAddress = request.AddressRequestDto.ToString();
        if (office.RegistryPhoneNumber == request.RegistryPhoneNumber && office.Address == stringAddress)
        {
            return ResultBuilder.BadRequest(ErrorMessages.NothingChanged);
        }
        
        
        office.UpdateOffice(request.OfficeId, stringAddress, request.RegistryPhoneNumber );
        await writeOfficesRepository.UpdateOfficeAsync(office, cancellationToken);
        
        var officeViewDto = OfficeWithoutPhotoViewDto.MapFromModel(office);
        
        return ResultBuilder.Success().WithData(officeViewDto).WithStatusCode(200);
    }
}