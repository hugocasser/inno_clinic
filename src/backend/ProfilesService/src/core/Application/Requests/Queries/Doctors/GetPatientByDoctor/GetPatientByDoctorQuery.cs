using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Queries.Doctors.GetPatientByDoctor;

public record GetPatientByDoctorQuery(Guid Id) : IRequest<HttpRequestResult>;