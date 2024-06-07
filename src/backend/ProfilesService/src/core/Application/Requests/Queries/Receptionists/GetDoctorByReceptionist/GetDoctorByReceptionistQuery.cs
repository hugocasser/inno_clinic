using Application.Abstractions.CQRS;
using Application.OperationResult.Results;

namespace Application.Requests.Queries.Receptionists.GetDoctorByReceptionist;

public record GetDoctorByReceptionistQuery(Guid Id) : IRequest<HttpRequestResult>;