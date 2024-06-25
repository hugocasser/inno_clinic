using Application.Result;
using MediatR;

namespace Application.Requests.Queries.ViewAppointmentResult;

public class ViewAppointmentResultQueryHandler : IRequestHandler<ViewAppointmentResultQuery, OperationResult>
{
    public Task<OperationResult> Handle(ViewAppointmentResultQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}