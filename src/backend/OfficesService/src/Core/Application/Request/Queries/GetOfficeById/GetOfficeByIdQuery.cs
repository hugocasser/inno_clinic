using Application.Abstractions.OperationResult;
using MediatR;

namespace Application.Request.Queries.GetOfficeById;

public record GetOfficeByIdQuery(Guid Id) : IRequest<IResult>;