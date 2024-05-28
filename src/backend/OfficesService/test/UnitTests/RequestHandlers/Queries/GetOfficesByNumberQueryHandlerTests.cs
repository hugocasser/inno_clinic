using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Persistence.Repositories.Specification;
using Application.Abstractions.Services.ExternalServices;
using Application.Dtos.Requests;
using Application.Dtos.View;
using Application.Request.Queries.GetOfficesByNumber;

namespace UnitTests.RequestHandlers.Queries;

public class GetOfficesByNumberQueryHandlerTests
{
    private readonly string _randomString = Guid.NewGuid().ToString();
    private readonly Mock<IReadOfficesRepository> _readOfficesRepository = new();
    private readonly Mock<IPhotoService> _photoService = new();
    
    [Fact]
    public async Task GetOfficesByAddress_ShouldReturnSuccessWithData_WhenOfficesExists()
    {
        // Arrange
        var pageSettings = Utilities.GenerateValidPageSettings();
        var query = new GetOfficesByNumberQuery(_randomString, pageSettings, false);
        var offices = Utilities.GenerateOfficesList(pageSettings.ItemsPerPage).ToList();
        
        _readOfficesRepository.Setup(repo => 
            repo.GetManyByAsync(It.IsAny<IBaseSpecification<Office>>(),
                It.IsAny<PageSettings>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(offices.AsReadOnly());
        
        _photoService
            .Setup(x => x.UploadPhotoInBase64Async(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ResultBuilder.Success().WithData(Guid.NewGuid().ToString()).WithStatusCode(200));
        var handler = new GetOfficesByNumberQueryHandler(_readOfficesRepository.Object, _photoService.Object);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.Success()
            .WithData(offices
                .Select(OfficeWithoutPhotoViewDto.MapFromModel)).WithStatusCode(200));
    }
    
    [Fact]
    public async Task GetOfficesByAddress_ShouldReturnSuccessWithoutData_WhenNoOfficesExists()
    {
        // Arrange
        _readOfficesRepository.Setup(repo => 
            repo.GetManyByAsync(It.IsAny<IBaseSpecification<Office>>(),
                It.IsAny<PageSettings>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as IReadOnlyList<Office>);
        
        var pageSettings = Utilities.GenerateValidPageSettings();
        var query = new GetOfficesByNumberQuery(_randomString, pageSettings, false);
        var handler = new GetOfficesByNumberQueryHandler(_readOfficesRepository.Object, _photoService.Object);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.Success().WithStatusCode(204));
    }
}