using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.RollbackPatientsProfile;

public record RollbackPatientsProfileCommand(Guid PatientId) : IRequest<HttpRequestResult>;