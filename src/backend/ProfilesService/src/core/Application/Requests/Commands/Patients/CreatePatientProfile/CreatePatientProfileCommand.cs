using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Patients.CreatePatientProfile;

public record CreatePatientProfileCommand() : IRequest<HttpRequestResult>;