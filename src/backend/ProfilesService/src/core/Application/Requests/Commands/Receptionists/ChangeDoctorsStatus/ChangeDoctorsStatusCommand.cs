using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.ChangeDoctorsStatus;

public record ChangeDoctorsStatusCommand(Guid DoctorId, Guid StatusId) : IRequest<HttpRequestResult>;