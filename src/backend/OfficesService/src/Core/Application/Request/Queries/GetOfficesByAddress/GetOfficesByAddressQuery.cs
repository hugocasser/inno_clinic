using Application.Abstractions.OperationResult;
using Application.Dtos.Requests;
using MediatR;

namespace Application.Request.Queries.GetOfficesByAddress;

public record GetOfficesByAddressQuery(string Address, PageSettings PageSettings, bool OnlyActive) : IRequest<IResult>
{
    
}