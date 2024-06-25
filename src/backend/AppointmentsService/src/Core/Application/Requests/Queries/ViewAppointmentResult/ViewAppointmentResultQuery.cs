using Application.Dtos;
using Application.Result;
using MediatR;

namespace Application.Requests.Queries.ViewAppointmentResult;

public record ViewAppointmentResultQuery(Guid Id, EnumRoles Role, Guid UserId) : IRequest<OperationResult>;