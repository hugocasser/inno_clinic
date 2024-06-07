using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Receptionists.DeleteReceptionist;

public record DeleteReceptionistCommand(Guid ReceptionistId) : IRequest<HttpRequestResult>;