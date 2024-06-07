using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Dtos.View;
using Application.OperationResults;
using Application.Services.Specification.Offices;
using MediatR;

namespace Application.Request.Queries.GetActiveOffices;

public class GetActiveOfficesQueryHandler(IReadOfficesRepository officesRepository) : IRequestHandler<GetActiveOfficesQuery, IResult>
{
    public async Task<IResult> Handle(GetActiveOfficesQuery request, CancellationToken cancellationToken)
    {
        var offices = await officesRepository
            .GetManyByAsync(new ActiveOffices(), request.PageSettings, cancellationToken);

        if (offices != null)
        {
            return ResultBuilder.Success().WithData(offices.Select(OfficeWithoutPhotoViewDto.MapFromModel))
                .WithStatusCode(200);
        }
        
        return ResultBuilder.Success().WithStatusCode(204);
    }
}