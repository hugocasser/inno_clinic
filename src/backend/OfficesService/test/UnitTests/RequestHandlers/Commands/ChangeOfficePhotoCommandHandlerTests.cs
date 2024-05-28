using Application.Abstractions.Persistence.Repositories;
using Application.Dtos.View;
using Application.Request.Commands.ChangeOfficePhoto;

namespace UnitTests.RequestHandlers.Commands;

public class ChangeOfficePhotoCommandHandlerTests
{
    private readonly Mock<IWriteOfficesRepository> _writeOfficesRepositoryMock = new ();

    [Fact]
    public async Task ChangeOfficePhoto_ShouldReturnSuccess_WhenOfficeExists()
    {
        // Arrange
        var office = Utilities.GenerateOfficeWithPhoto();
        
        _writeOfficesRepositoryMock
            .Setup(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(office)
            .Verifiable();
        
        _writeOfficesRepositoryMock
            .Setup(x => x.UpdateOfficeAsync(It.IsAny<Domain.Models.Office>()))
            .Returns(Task.CompletedTask)
            .Verifiable();
        
        _writeOfficesRepositoryMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();
        
        var handler = new ChangeOfficePhotoCommandHandler(_writeOfficesRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(new ChangeOfficePhotoCommand(Guid.NewGuid(), Guid.NewGuid()), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.GetStatusCode().Should().Be(200);
        result.GetOperationResult().Should().BeEquivalentTo(OfficeWithoutPhotoViewDto.MapFromModel(office));

        _writeOfficesRepositoryMock.Verify(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        _writeOfficesRepositoryMock.Verify(x => x.UpdateOfficeAsync(It.IsAny<Domain.Models.Office>()), Times.Once);
        _writeOfficesRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task ChangeOfficePhoto_ShouldReturnNotFound_WhenOfficeDoesNotExist()
    {
        // Arrange
        _writeOfficesRepositoryMock
            .Setup(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Models.Office?) null)
            .Verifiable();
        
        var handler = new ChangeOfficePhotoCommandHandler(_writeOfficesRepositoryMock.Object);
        
        // Act
        var result = await handler.Handle(new ChangeOfficePhotoCommand(Guid.NewGuid(), Guid.NewGuid()), CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.NotFound(ErrorMessages.OfficeNotFound));
        
        _writeOfficesRepositoryMock.Verify(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }

    [Fact]
    public async Task ChangeOfficePhoto_ShouldReturnBadRequest_WhenNoChanges()
    {
        // Arrange
        var office = Utilities.GenerateOfficeWithPhoto();

        _writeOfficesRepositoryMock
            .Setup(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(office)
            .Verifiable();

        var handler = new ChangeOfficePhotoCommandHandler(_writeOfficesRepositoryMock.Object);

        // Act
        var result = await handler.Handle(new ChangeOfficePhotoCommand(office.Id, office.PhotoId),
            CancellationToken.None);

        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.BadRequest(ErrorMessages.NothingChanged));

        _writeOfficesRepositoryMock.Verify(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
            Times.Once);
    }
}