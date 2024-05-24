using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Dtos;
using Application.OperationResults;
using Application.Services.Specification.Offices;
using MediatR;

namespace Application.Request.Queries.GetActiveOffices;

public class GetActiveOfficesQueryHandler(IReadOfficesRepository officesRepository) : IRequestHandler<GetActiveOfficesQuery, IResult>
{
    public async Task<IResult> Handle(GetActiveOfficesQuery request, CancellationToken cancellationToken)
    {
        var offices = await officesRepository
            .GetManyByAsync<OfficeWithoutPhotoViewDto>(new ActiveOffices(), request.PageSettings, cancellationToken);
        
        return ResultBuilder.Success().WithData(offices).WithStatusCode(200);
    }
}