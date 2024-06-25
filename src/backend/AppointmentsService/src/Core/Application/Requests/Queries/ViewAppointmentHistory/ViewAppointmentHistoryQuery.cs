using Application.Dtos;
using Application.Dtos.Requests;
using Application.Result;
using MediatR;

namespace Application.Requests.Queries.ViewAppointmentHistory;

public record ViewAppointmentHistoryQuery(Guid Id, EnumRoles Role, Guid UserId, PageSetting Page) : IRequest<OperationResult>;