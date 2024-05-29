using Application.Abstractions.OperationResult;
using Application.Dtos.Requests;
using MediatR;

namespace Application.Request.Queries.GetOffices;

public record GetOfficesQuery(PageSettings PageSettings, bool WithPhotos) : IRequest<IResult>;