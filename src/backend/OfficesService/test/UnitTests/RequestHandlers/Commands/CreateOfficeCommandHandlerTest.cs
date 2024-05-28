using Application.Abstractions.Persistence.Repositories;
using Application.Dtos.Requests;
using Application.Dtos.View;
using Application.Request.Commands.CreateOffice;

namespace UnitTests.RequestHandlers.Commands;

public class CreateOfficeCommandHandlerTest
{
    private readonly Mock<IWriteOfficesRepository> _writeOfficesRepositoryMock = new ();

    [Fact]
    public async Task CreateOffice_ShouldReturnSuccess_WhenOfficeNotExists()
    {
        // Arrange
        var office = Utilities.GenerateOfficeWithoutPhoto();
        
        _writeOfficesRepositoryMock
            .Setup(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as Office)
            .Verifiable();
        
        _writeOfficesRepositoryMock
            .Setup(x => x.AddOfficeAsync(It.IsAny<Office>(), It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        _writeOfficesRepositoryMock
            .Setup(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .Returns(Task.CompletedTask)
            .Verifiable();

        var handler = new CreateOfficeCommandHandler(_writeOfficesRepositoryMock.Object);
        var addressRequestDto = new AddressRequestDto("street", "city", "state", "zip", "country");
        // Act
        var result = await handler.Handle(new CreateOfficeCommand(addressRequestDto, office.RegistryPhoneNumber, office.IsActive, office.PhotoId), CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.GetStatusCode().Should().Be(200);
        result.GetOperationResult().Should().BeOfType<OfficeWithoutPhotoViewDto>();

        _writeOfficesRepositoryMock.Verify(x => x.AddOfficeAsync(It.IsAny<Office>(), It.IsAny<CancellationToken>()), Times.Once);
        _writeOfficesRepositoryMock.Verify(x => x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}