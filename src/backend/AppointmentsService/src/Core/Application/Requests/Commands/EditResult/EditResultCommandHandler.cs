using Application.Result;
using MediatR;

namespace Application.Requests.Commands.EditResult;

public class EditResultCommandHandler : IRequestHandler<EditResultCommand, OperationResult>
{
    public Task<OperationResult> Handle(EditResultCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}