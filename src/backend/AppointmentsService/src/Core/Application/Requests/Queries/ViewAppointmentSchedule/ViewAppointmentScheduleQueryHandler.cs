using Application.Result;
using MediatR;

namespace Application.Requests.Queries.ViewAppointmentSchedule;

public class ViewAppointmentScheduleQueryHandler : IRequestHandler<ViewAppointmentScheduleQuery, OperationResult>
{
    public Task<OperationResult> Handle(ViewAppointmentScheduleQuery request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}