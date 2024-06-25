using Application.Result;
using MediatR;

namespace Application.Requests.Queries.ViewAppointmentsList;

public record ViewAppointmentsListQuery() : IRequest<OperationResult>;