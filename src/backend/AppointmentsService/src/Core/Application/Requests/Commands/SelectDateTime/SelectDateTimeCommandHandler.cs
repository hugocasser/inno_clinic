using Application.Result;
using MediatR;

namespace Application.Requests.Commands.SelectDateTime;

public class SelectDateTimeCommandHandler : IRequestHandler<SelectDateTimeCommand, OperationResult>
{
    public Task<OperationResult> Handle(SelectDateTimeCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}