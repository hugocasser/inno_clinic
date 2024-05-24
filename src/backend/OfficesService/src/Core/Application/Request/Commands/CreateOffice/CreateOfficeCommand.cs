using Application.Abstractions.OperationResult;
using Application.Dtos.Requests;
using Google.Type;
using MediatR;

namespace Application.Request.Commands.CreateOffice;

public record CreateOfficeCommand
    (AddressRequestDto AddressRequestDto, string RegistryPhoneNumber, bool IsActive, Guid? PhotoId)
    : IRequest<IResult>;