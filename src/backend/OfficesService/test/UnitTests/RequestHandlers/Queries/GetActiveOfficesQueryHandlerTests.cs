using Application.Abstractions.Persistence.Repositories;
using Application.Abstractions.Persistence.Repositories.Specification;
using Application.Dtos.Requests;
using Application.Dtos.View;
using Application.Request.Queries.GetActiveOffices;

namespace UnitTests.RequestHandlers.Queries;

public class GetActiveOfficesQueryHandlerTests
{
    private readonly Mock<IReadOfficesRepository> _readOfficesRepository = new();

    [Fact]
    public async Task GetActiveOffices_ShouldReturnSuccessWithData_WhenOfficesExists()
    {
        // Arrange
        var pageSettings = Utilities.GenerateValidPageSettings();
        var offices = Utilities.GenerateOfficesList(pageSettings.ItemsPerPage)
            .ToList().AsReadOnly();
        
        _readOfficesRepository.Setup(repo => 
            repo.GetManyByAsync(It.IsAny<IBaseSpecification<Office>>(),
                It.IsAny<PageSettings>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(offices);
                
        var query = new GetActiveOfficesQuery(pageSettings);
        var handler = new GetActiveOfficesQueryHandler(_readOfficesRepository.Object);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.Success()
            .WithData(offices.Select(OfficeWithoutPhotoViewDto.MapFromModel)).WithStatusCode(200));
    }

    [Fact]
    public async Task GetActiveOffices_ShouldReturnSuccessWithoutData_WhenNoOfficesExists()
    {
        var pageSettings = Utilities.GenerateValidPageSettings();
        var query = new GetActiveOfficesQuery(pageSettings);
        
        _readOfficesRepository.Setup(repo => 
            repo.GetManyByAsync(It.IsAny<IBaseSpecification<Office>>(),
                It.IsAny<PageSettings>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(null as IReadOnlyList<Office>);
        
        var handler = new GetActiveOfficesQueryHandler(_readOfficesRepository.Object);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        
        // Assert
        result.Should().BeEquivalentTo(ResultBuilder.Success().WithStatusCode(204));
    }
}