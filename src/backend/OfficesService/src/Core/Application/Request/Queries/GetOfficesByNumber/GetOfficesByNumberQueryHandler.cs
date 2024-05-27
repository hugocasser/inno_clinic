using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Persistence.Repositories.Specification;
using Application.Abstractions.Services.ExternalServices;
using Application.Dtos.View;
using Application.OperationResults;
using Application.Services.Specification.Offices;
using Domain.Models;
using MediatR;

namespace Application.Request.Queries.GetOfficesByNumber;

public class GetOfficesByNumberQueryHandler
    (IReadOfficesRepository officesRepository, IPhotoService photoService)
    : IRequestHandler<GetOfficesByNumberQuery, IResult>
{
    public async Task<IResult> Handle(GetOfficesByNumberQuery request, CancellationToken cancellationToken)
    {
        IBaseSpecification<Office> specification;
        
        if (request.OnlyActive)
        {
            specification = new OfficesByNumber(request.Number) & new ActiveOffices();
        }
        else
        {
            specification = new OfficesByNumber(request.Number);
        }
        
        var offices = await officesRepository
            .GetManyByAsync(specification, request.PageSettings, cancellationToken);
        if (offices == null)
        {
            return ResultBuilder.Success().WithStatusCode(204);
        }
        
        
        var officesPhotos = new List<string?>();
        
        foreach (var office in offices)
        {
            if (office == null)
            {
                officesPhotos.Add(ErrorMessages.OfficeNotFound);
            }

            var photo = await photoService
                .UploadPhotoInBase64Async(office.PhotoId, cancellationToken);

            if (photo.IsSuccess)
            {
                officesPhotos.Add(photo.ResultData as string);
            }
            else
            {
                officesPhotos.Add(ErrorMessages.PhotoNotFound);
            }
        }
        
        var views = offices.Select((t, i) => 
            OfficeWithPhotoViewDto.MapFromModel(t, officesPhotos[i])).ToList();

        return ResultBuilder.Success().WithData(offices).WithStatusCode(200);
    }
}