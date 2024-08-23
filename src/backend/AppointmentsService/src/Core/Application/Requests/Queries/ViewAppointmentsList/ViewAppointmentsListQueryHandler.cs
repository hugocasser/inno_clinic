using Application.Result;
using MediatR;

namespace Application.Requests.Queries.ViewAppointmentsList;

public class ViewAppointmentsListQueryHandler : IRequestHandler<ViewAppointmentsListQuery, OperationResult>
{
    public Task<OperationResult> Handle(ViewAppointmentsListQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}