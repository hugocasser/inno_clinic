using Application.Abstractions.OperationResult;
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Abstractions.Services.ValidationServices;
using Application.Dtos;
using Application.OperationResults;
using Domain.Models;
using MediatR;

namespace Application.Request.Commands.CreateOffice;

public class CreateOfficeCommandHandler(IWriteOfficesRepository writeOfficesRepository, IGoogleMapsApiClient googleMapsApiClient,
    IPhoneValidatorService phoneValidatorService)
    : IRequestHandler<CreateOfficeCommand, IResult>
{
    public async Task<IResult> Handle(CreateOfficeCommand request, CancellationToken cancellationToken)
    {
        var office = Office.CreateOffice(request.Address, request.RegistryPhoneNumber, request.IsActive);
        await writeOfficesRepository.AddOfficeAsync(office);
        
        var officeViewDto = OfficeWithoutPhotoViewDto.MapFromModel(office);

        return ResultBuilder.Success().WithData(officeViewDto).WithStatusCode(200);
    }
}