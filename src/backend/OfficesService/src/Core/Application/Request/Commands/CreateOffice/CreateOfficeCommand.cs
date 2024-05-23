using Application.Abstractions.OperationResult;
using MediatR;

namespace Application.Request.Commands.CreateOffice;

public record CreateOfficeCommand(string Address, string RegistryPhoneNumber, bool IsActive, Guid? PhotoId) : IRequest<IResult>;