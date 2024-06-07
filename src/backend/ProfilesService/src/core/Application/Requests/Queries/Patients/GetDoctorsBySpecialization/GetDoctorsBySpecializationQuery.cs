using Application.Abstractions.CQRS;
using Application.Dtos.Requests;
using Application.OperationResult.Results;

namespace Application.Requests.Queries.Patients.GetDoctorsBySpecialization;

public record GetDoctorsBySpecializationQuery(string Specialization, PageSettings PageSettings) : IRequest<HttpRequestResult>;