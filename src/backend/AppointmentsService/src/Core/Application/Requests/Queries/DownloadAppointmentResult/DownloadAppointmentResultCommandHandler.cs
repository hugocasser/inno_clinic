using Application.Result;
using MediatR;

namespace Application.Requests.Queries.DownloadAppointmentResult;

public class DownloadAppointmentResultCommandHandler : IRequestHandler<DownloadAppointmentResultCommand, OperationResult>
{
    public Task<OperationResult> Handle(DownloadAppointmentResultCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}