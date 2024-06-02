using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.DeletePatientsProfile;

public record DeletePatientsProfileCommand(Guid PatientId) : IRequest<HttpRequestResult>;