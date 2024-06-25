using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointmentResult;

public class CreateAppointmentResultCommandHandler : IRequestHandler<CreateAppointmentResultCommand, OperationResult>
{
    public Task<OperationResult> Handle(CreateAppointmentResultCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}