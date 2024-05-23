using Application.Abstractions.OperationResult;
using MediatR;

namespace Application.Request.Commands.ChangeOfficePhoto;

public record ChangeOfficePhotoCommand(Guid OfficeId, Guid PhotoId) : IRequest<IResult>;