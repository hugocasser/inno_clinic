using Application.Abstractions.OperationResult;
using Application.Dtos.Requests;
using MediatR;

namespace Application.Request.Queries.GetInactiveOffices;

public record GetInactiveOfficesQuery(PageSettings PageSettings) : IRequest<IResult>;