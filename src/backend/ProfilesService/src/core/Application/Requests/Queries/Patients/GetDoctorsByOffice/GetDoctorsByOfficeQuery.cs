using Application.Abstractions.CQRS;
using Application.Dtos.Requests;
using Application.OperationResult.Results;

namespace Application.Requests.Queries.Patients.GetDoctorsByOffice;

public record GetDoctorsByOfficeQuery(Guid OfficeId, PageSettings PageSettings) : IRequest<HttpRequestResult>;