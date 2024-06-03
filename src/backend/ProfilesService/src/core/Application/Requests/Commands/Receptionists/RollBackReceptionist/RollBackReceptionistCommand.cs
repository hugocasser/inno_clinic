using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.RollBackReceptionist;

public record RollBackReceptionistCommand(Guid ReceptionistId) : IRequest<HttpRequestResult>;