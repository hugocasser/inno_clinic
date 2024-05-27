using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Services.ValidationServices;
using Application.Dtos;
using Application.Dtos.View;
using Application.OperationResults;
using Domain.Models;
using MediatR;

namespace Application.Request.Commands.CreateOffice;

public class CreateOfficeCommandHandler
    (IWriteOfficesRepository writeOfficesRepository,
        IGoogleMapsApiClient googleMapsApiClient,
        IPhoneValidatorService phoneValidatorService)
        : IRequestHandler<CreateOfficeCommand, IResult>
{
    public async Task<IResult> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var stringAddress = request.AddressRequestDto.ToString();
        
        var office = Office.CreateOffice(stringAddress, request.RegistryPhoneNumber, request.IsActive);
        await writeOfficesRepository.AddOfficeAsync(office, cancellationToken);
        
        var officeViewDto = OfficeWithoutPhotoViewDto.MapFromModel(office);

        return ResultBuilder.Success().WithData(officeViewDto).WithStatusCode(200);
    }
}