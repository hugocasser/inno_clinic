using Application.Abstractions.OperationResult;
using Application.Dtos.Requests;
using MediatR;

namespace Application.Request.Queries.GetOfficesByNumber;

public record GetOfficesByNumberQuery(string Number, PageSettings PageSettings, bool OnlyActive) : IRequest<IResult>;