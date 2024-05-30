using Application.Abstractions.Persistence.Repositories;
using Application.Dtos.View;
using Application.Request.Commands.ChangeOfficeStatus;

namespace UnitTests.RequestHandlers.Commands;

public class ChangeOfficeStatusCommandHandlerTests
{
    private readonly Mock<IWriteOfficesRepository> _writeOfficesRepositoryMock = new ();

    [Fact]
    public async Task ChangeOfficeStatus_ShouldReturnSuccess_WhenOfficeExists()
    {
        // Arrange
        var office = Utilities.GenerateOfficeWithPhoto();

        _writeOfficesRepositoryMock
            .Setup(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(office)
            .Verifiable();

        _writeOfficesRepositoryMock
            .Setup(x => x.UpdateOfficeAsync(It.IsAny<Office>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        _writeOfficesRepositoryMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();
        
        var handler = new ChangeOfficeStatusCommandHandler(_writeOfficesRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(new ChangeOfficeStatusCommand(office.Id, !office.IsActive), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.GetStatusCode().Should().Be(200);
        result.GetOperationResult().Should().BeEquivalentTo(OfficeWithoutPhotoViewDto.MapFromModel(office));
    }
    
    [Fact]
    public async Task ChangeOfficeStatus_ShouldReturnNotFound_WhenOfficeDoesNotExist()
    {
        // Arrange
        _writeOfficesRepositoryMock
            .Setup(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Office?) null)
            .Verifiable();
        
        var handler = new ChangeOfficeStatusCommandHandler(_writeOfficesRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(new ChangeOfficeStatusCommand(Guid.NewGuid(), false), CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.NotFound(ErrorMessages.OfficeNotFound));
    }
    
    [Fact]
    public async Task ChangeOfficeStatus_ShouldReturnBadRequest_WhenStatusIsTheSame()
    {
        // Arrange
        var office = Utilities.GenerateOfficeWithPhoto();
        
        _writeOfficesRepositoryMock
            .Setup(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(office)
            .Verifiable();
        
        var handler = new ChangeOfficeStatusCommandHandler(_writeOfficesRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(new ChangeOfficeStatusCommand(office.Id, office.IsActive), CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.BadRequest(ErrorMessages.NothingChanged));
    }
}