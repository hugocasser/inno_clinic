using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointmentByReceptionist;

public class CreateAppointmentByReceptionistCommandHandler
    : IRequestHandler<CreateAppointmentByReceptionistCommand, OperationResult>
{
    public Task<OperationResult> Handle(CreateAppointmentByReceptionistCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}