using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Services.ExternalServices;
using Application.Dtos.View;
using Application.OperationResults;
using Application.Services.Specification.Offices;
using MediatR;

namespace Application.Request.Queries.GetOfficeById;

public class GetOfficeByIdQueryHandler
    (IReadOfficesRepository repository,
        IPhotoService photoService) : IRequestHandler<GetOfficeByIdQuery, IResult>
{
    public async Task<IResult> Handle(GetOfficeByIdQuery request, CancellationToken cancellationToken)
    {
        var office = await repository.GetByAsync(new OfficeById(request.Id),cancellationToken);

        if (office is null)
        {
            return ResultBuilder.NotFound(ErrorMessages.OfficeNotFound);
        }

        var officePhoto = await photoService
            .UploadPhotoInBase64Async(office.PhotoId, cancellationToken);

        return !officePhoto.IsSuccess ?
            ResultBuilder.Success().WithData(OfficeWithoutPhotoViewDto.MapFromModel(office))
            : ResultBuilder.Success().WithData(OfficeWithPhotoViewDto
                .MapFromModel(office, (officePhoto.GetOperationResult() as string)!));
    }
}