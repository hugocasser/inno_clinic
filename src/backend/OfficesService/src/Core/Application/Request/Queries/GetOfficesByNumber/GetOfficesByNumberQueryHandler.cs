using Application.Abstractions.OperationResult;
using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Persistence.Repositories.Specification;
using Application.Dtos;
using Application.OperationResults;
using Application.Services.Specification.Offices;
using Domain.Models;
using MediatR;

namespace Application.Request.Queries.GetOfficesByNumber;

public class GetOfficesByNumberQueryHandler(IReadOfficesRepository officesRepository) : IRequestHandler<GetOfficesByNumberQuery, IResult>
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
            .GetManyByAsync<OfficeWithoutPhotoViewDto>(specification, request.PageSettings, cancellationToken);
        
        return ResultBuilder.Success().WithData(offices).WithStatusCode(200);
    }
}