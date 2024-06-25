using Application.Result;
using MediatR;

namespace Application.Requests.Commands.ApproveAppointment;

public class ApproveAppointmentCommandHandler : IRequestHandler<ApproveAppointmentCommand, OperationResult>
{
    public Task<OperationResult> Handle(ApproveAppointmentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}