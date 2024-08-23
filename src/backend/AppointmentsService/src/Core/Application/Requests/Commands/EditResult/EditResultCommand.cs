using Application.Result;
using MediatR;

namespace Application.Requests.Commands.EditResult;

public record EditResultCommand
    (Guid Id, string Complaints, string Conclusion, string Recommendation) : IRequest<OperationResult>;