using Application.Abstractions.OperationResult;
using Application.Dtos.Requests;
using MediatR;

namespace Application.Request.Queries.GetActiveOffices;

public record GetActiveOfficesQuery(PageSettings PageSettings) : IRequest<IResult>;