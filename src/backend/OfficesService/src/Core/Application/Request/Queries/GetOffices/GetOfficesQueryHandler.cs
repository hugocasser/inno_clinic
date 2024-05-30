using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Services.ExternalServices;
using Application.Dtos.View;
using Application.OperationResults;
using Application.Services.Specification.Offices;
using Domain.Models;
using MediatR;

namespace Application.Request.Queries.GetOffices;

public class GetOfficesQueryHandler
    (IReadOfficesRepository repository, IPhotoService photoService): IRequestHandler<GetOfficesQuery, IResult>
{
    public async Task<IResult> Handle(GetOfficesQuery request, CancellationToken cancellationToken)
    {
        var offices = await repository.GetManyByAsync(new AllOffices(), request.PageSettings, cancellationToken);

        if (offices is null)
        {
            return ResultBuilder.Success();
        }

        var officesViewList = await GetOfficesViewListAsync(offices, request.WithPhotos, cancellationToken);
    
        return ResultBuilder
            .Success()
            .WithData(officesViewList)
            .WithStatusCode(200);
    }

    private async Task<List<object>> GetOfficesViewListAsync(IReadOnlyList<Office?> offices, bool withPhotos, CancellationToken cancellationToken)
    {
        var officesViewList = new List<object>();

        if (withPhotos)
        {
            var photoTasks = offices.Select(office => photoService.UploadPhotoInBase64Async(office!.PhotoId, cancellationToken));
            var photos = await Task.WhenAll(photoTasks);

            for (var i = 0; i < offices.Count; i++)
            {
                var photo = photos[i];

                officesViewList.Add(photo.IsSuccess
                    ? OfficeWithPhotoViewDto.MapFromModel(offices[i]!, (photo.GetOperationResult() as string)!)
                    : OfficeWithPhotoViewDto.MapFromModel(offices[i]!, "Error while loading photo"));
            }
        }
        else
        {
            officesViewList.AddRange(offices.Select(OfficeWithoutPhotoViewDto.MapFromModel!));
        }

        return officesViewList;
    }
}