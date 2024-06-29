using Application.Result;
using MediatR;

namespace Application.Requests.Commands.CreateAppointmentResult;

public record CreateAppointmentResultCommand
    (string Complaints,
        string Conclusion,
        string Recommendation, 
        Guid AppointmentId) : IRequest<OperationResult>
{
    public Domain.Models.Result MapToResult()
    {
        var result = new Domain.Models.Result
        {
            Id = Guid.NewGuid(),
            Complaints = Complaints,
            Conclusion = Conclusion,
            Recommendation = Recommendation,
            AppointmentId = AppointmentId,
            IsDeleted = false
        };
        
        return result;
    }
}