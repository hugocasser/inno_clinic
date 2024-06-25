using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CancelAppointments;

public class CancelAppointmentCommandHandler : IRequestHandler<CancelAppointmentCommand, OperationResult>
{
    public Task<OperationResult> Handle(CancelAppointmentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}