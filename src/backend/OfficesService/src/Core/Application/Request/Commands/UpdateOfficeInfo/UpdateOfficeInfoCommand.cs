using Application.Abstractions.OperationResult;
using MediatR;

namespace Application.Request.Commands.UpdateOfficeInfo;

public record UpdateOfficeInfoCommand
    (Guid OfficeId, string Address, string RegistryPhoneNumber) 
    : IRequest<IResult>;