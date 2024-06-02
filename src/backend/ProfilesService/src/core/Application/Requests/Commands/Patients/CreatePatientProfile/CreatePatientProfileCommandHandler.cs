using Application.Abstractions.CQRS;
using Application.OperationResult.Builders;
using Application.OperationResult.Results;

namespace Application.Requests.Commands.Patients.CreatePatientProfile;

public class CreatePatientProfileCommandHandler : IRequestHandler<CreatePatientProfileCommand, HttpRequestResult>
{
    public async Task<HttpRequestResult> HandleAsync(CreatePatientProfileCommand request, CancellationToken cancellationToken = default)
    {
        return HttpResultBuilder.NoContent();
    }
}