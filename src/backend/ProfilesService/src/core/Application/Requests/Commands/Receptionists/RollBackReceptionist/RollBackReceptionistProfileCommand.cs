using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.RollBackReceptionist;

public record RollBackReceptionistProfileCommand(Guid ReceptionistId) : IRequest<HttpRequestResult>;