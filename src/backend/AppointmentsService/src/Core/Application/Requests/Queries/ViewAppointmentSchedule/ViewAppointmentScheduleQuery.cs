using Application.Result;
using MediatR;

namespace Application.Requests.Queries.ViewAppointmentSchedule;

public record ViewAppointmentScheduleQuery(Guid Id) : IRequest<OperationResult>;