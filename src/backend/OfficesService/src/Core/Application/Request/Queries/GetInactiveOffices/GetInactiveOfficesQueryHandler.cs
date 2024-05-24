using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Dtos;
using Application.OperationResults;
using Application.Services.Specification.Offices;
using MediatR;

namespace Application.Request.Queries.GetInactiveOffices;

public class GetInactiveOfficesQueryHandler(IReadOfficesRepository officesRepository)
    : IRequestHandler<GetInactiveOfficesQuery, IResult>
{
    public async Task<IResult> Handle(GetInactiveOfficesQuery request, CancellationToken cancellationToken)
    {
        var offices = await officesRepository
            .GetManyByAsync<OfficeWithoutPhotoViewDto>(new InactiveOffices(), request.PageSettings, cancellationToken);
        
        return ResultBuilder.Success().WithData(offices).WithStatusCode(200);
    }
}