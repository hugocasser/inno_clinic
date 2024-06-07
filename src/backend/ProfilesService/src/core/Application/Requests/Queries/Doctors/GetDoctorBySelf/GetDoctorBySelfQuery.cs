using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Queries.Doctors.GetDoctorBySelf;

public record GetDoctorBySelfQuery(Guid Id) : IRequest<HttpRequestResult>;