using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointment;

public class CreateAppointmentCommandHandler : IRequestHandler<CreateAppointmentCommand, OperationResult>
{
    public Task<OperationResult> Handle(CreateAppointmentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}