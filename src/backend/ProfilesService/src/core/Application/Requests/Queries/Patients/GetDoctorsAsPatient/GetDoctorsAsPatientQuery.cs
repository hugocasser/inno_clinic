using Application.Abstractions.CQRS;
using Application.Dtos.Requests;
using Application.OperationResult.Results;

namespace Application.Requests.Queries.Patients.GetDoctorsAsPatient;

public record GetDoctorsAsPatientQuery
    (PageSettings PageSettings, string? FullName, int MinExperience = 0, int MaxExperience = 0) : IRequest<HttpRequestResult>;