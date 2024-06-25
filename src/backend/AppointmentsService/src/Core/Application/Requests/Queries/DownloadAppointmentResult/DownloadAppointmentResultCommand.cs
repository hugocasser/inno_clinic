using Application.Result;
using MediatR;

namespace Application.Requests.Queries.DownloadAppointmentResult;

public record DownloadAppointmentResultCommand(Guid Id) : IRequest<OperationResult>;