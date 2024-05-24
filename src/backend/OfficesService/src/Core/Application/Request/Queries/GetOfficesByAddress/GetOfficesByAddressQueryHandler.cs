using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Persistence.Repositories.Specification;
using Application.Dtos;
using Application.OperationResults;
using Application.Services.Specification.Offices;
using Domain.Models;
using MediatR;

namespace Application.Request.Queries.GetOfficesByAddress;

public class GetOfficesByAddressQueryHandler(IReadOfficesRepository officesRepository)
    : IRequestHandler<GetOfficesByAddressQuery, IResult>
{
    public async Task<IResult> Handle(GetOfficesByAddressQuery request, CancellationToken cancellationToken)
    {
        IBaseSpecification<Office> specification;
        
        if (request.OnlyActive)
        {
            specification = new OfficesByAddress(request.Address) & new ActiveOffices();
        }
        else
        {
            specification = new OfficesByAddress(request.Address);
        }
        
        var offices = await officesRepository
            .GetManyByAsync<OfficeWithoutPhotoViewDto>(specification, request.PageSettings, cancellationToken);
        
        return ResultBuilder.Success().WithData(offices).WithStatusCode(200);
    }
}