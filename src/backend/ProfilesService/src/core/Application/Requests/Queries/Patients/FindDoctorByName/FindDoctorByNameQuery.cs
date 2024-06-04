using Application.Abstractions.CQRS;
using Application.Dtos.Requests;
using Application.OperationResult.Results;

namespace Application.Requests.Queries.Patients.FindDoctorByName;

public record FindDoctorByNameQuery(string Name, PageSettings PageSettings) : IRequest<HttpRequestResult>;