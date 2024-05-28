using Application.Abstractions.Persistence.Repositories;
using Application.Dtos.Requests;
using Application.Dtos.View;
using Application.Request.Commands.UpdateOfficeInfo;

namespace UnitTests.RequestHandlers.Commands;

public class UpdateOfficeInfoCommandHandlerTests
{
    private readonly Mock<IWriteOfficesRepository> _writeOfficesRepositoryMock = new ();
    
    [Fact]
    public async Task UpdateOfficeInfo_ShouldReturnNotFound_WhenOfficeDoesNotExist()
    {
        // Arrange
        _writeOfficesRepositoryMock
            .Setup(x => x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((Domain.Models.Office?) null)
            .Verifiable();
        
        var handler = new UpdateOfficeInfoCommandHandler(_writeOfficesRepositoryMock.Object);
        var addressRequestDto = new AddressRequestDto("street", "city", "state", "zip", "country");
        
        // Act
        var result = await handler.Handle(new UpdateOfficeInfoCommand(Guid.NewGuid(), addressRequestDto, "description"), CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.NotFound(ErrorMessages.OfficeNotFound));
        
        _writeOfficesRepositoryMock.Verify(x =>
            x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task UpdateOfficeInfo_ShouldReturnSuccess_WhenOfficeExists()
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
        
        var handler = new UpdateOfficeInfoCommandHandler(_writeOfficesRepositoryMock.Object);
        var addressRequestDto = new AddressRequestDto("street", "city", "state", "zip", "country");
        
        // Act
        var result = await handler.Handle(new UpdateOfficeInfoCommand(office.Id, addressRequestDto, "description"), CancellationToken.None);
        office.Address = addressRequestDto.ToString();
        office.RegistryPhoneNumber = "description";
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.Success().WithData(OfficeWithoutPhotoViewDto.MapFromModel(office)).WithStatusCode(200));
        
        _writeOfficesRepositoryMock.Verify(x =>
            x.GetOfficeAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()), Times.Once);
        
        _writeOfficesRepositoryMock.Verify(x =>
            x.UpdateOfficeAsync(It.IsAny<Domain.Models.Office>()), Times.Once);
        
        _writeOfficesRepositoryMock.Verify(x =>
            x.SaveChangesAsync(It.IsAny<CancellationToken>()), Times.Once);
    }
}