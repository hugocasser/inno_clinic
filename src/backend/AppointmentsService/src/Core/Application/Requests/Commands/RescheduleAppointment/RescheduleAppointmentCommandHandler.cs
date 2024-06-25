using Application.Result;
using MediatR;

namespace Application.Requests.Commands.RescheduleAppointment;

public class RescheduleAppointmentCommandHandler : IRequestHandler<RescheduleAppointmentCommand, OperationResult>
{
    public Task<OperationResult> Handle(RescheduleAppointmentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}