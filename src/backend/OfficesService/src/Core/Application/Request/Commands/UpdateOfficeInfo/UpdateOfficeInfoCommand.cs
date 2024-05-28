using Application.Abstractions.OperationResult;
using Application.Dtos.Requests;
using MediatR;

namespace Application.Request.Commands.UpdateOfficeInfo;

public record UpdateOfficeInfoCommand
    (Guid OfficeId, AddressRequestDto AddressRequestDto, string RegistryPhoneNumber) 
    : IRequest<IResult>;