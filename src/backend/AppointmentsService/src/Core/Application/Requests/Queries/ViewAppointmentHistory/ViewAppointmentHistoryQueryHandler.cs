using Application.Result;
using MediatR;

namespace Application.Requests.Queries.ViewAppointmentHistory;

public class ViewAppointmentHistoryQueryHandler : IRequestHandler<ViewAppointmentHistoryQuery, OperationResult>
{
    public Task<OperationResult> Handle(ViewAppointmentHistoryQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}