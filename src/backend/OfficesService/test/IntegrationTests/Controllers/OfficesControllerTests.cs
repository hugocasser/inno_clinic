using System.Net;
using System.Net.Http.Json;
using Application.Abstractions.Persistence.Repositories;
using Application.Dtos.View;
using Domain.Models;
using IntegrationTests.Fixtures;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Controllers;

    
    [Fact]
    public async Task GetOfficesWithPhotos_ReturnsOk()
    {
        // Arrange
        var offices = Utilities.GenerateOfficesList(10);
        var pageSettings = Utilities.GenerateValidPageSettings();
        await SeedOffices(offices);
        // Act
        var response = await _client.GetAsync($"api/offices/{true}/{pageSettings.Page}/{pageSettings.ItemsPerPage}");
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<OfficeWithPhotoViewDto>>();
        
        // Assert
        response.EnsureSuccessStatusCode();
        content.Select(office => office.PhotoBase64).Should().NotBeNullOrEmpty();
        content.Count().Should().Be(10);
    }
    
    [Fact]
    public async Task GetOfficesWithoutPhotos_ReturnsOk()
    {
        // Arrange
        var offices = Utilities.GenerateOfficesList(10);
        var pageSettings = Utilities.GenerateValidPageSettings();
        await SeedOffices(offices);
        // Act
        var response = await _client.GetAsync($"api/offices/{false}/{pageSettings.Page}/{pageSettings.ItemsPerPage}");
        var content = await response.Content.ReadFromJsonAsync<IEnumerable<OfficeWithoutPhotoViewDto>>();
        
        // Assert
        response.EnsureSuccessStatusCode();
        content.Count().Should().Be(10);
    }

    [Fact]
    public async Task GetOfficeById_ReturnsOk()
    {
        // Arrange
        var offices = Utilities.GenerateOfficesList(10);
        await SeedOffices(offices);
        
        // Act
        var response = await _client.GetAsync($"api/offices/{offices.First().Id}");
        var content = await response.Content.ReadFromJsonAsync<OfficeWithPhotoViewDto>();
        
        // Assert
        response.EnsureSuccessStatusCode();
        content.Should().BeEquivalentTo(OfficeWithPhotoViewDto.MapFromModel(offices.First(), "some base64 string"));
        content.PhotoBase64.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public async Task GetOfficeById_ReturnsNotFound()
    {
        // Arrange
        var offices = Utilities.GenerateOfficesList(10);
        await SeedOffices(offices);
        
        // Act
        var response = await _client.GetAsync($"api/offices/{Guid.NewGuid()}");
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Fact]
    public async Task CreateOffice_ReturnsOkWithOfficeView()
    {
        // Arrange
        var command = Utilities.GenerateOfficeCommand();
        
        // Act
        var response = await _client.PostAsJsonAsync("api/offices/", command);
        var content = await response.Content.ReadFromJsonAsync<OfficeWithoutPhotoViewDto>();
        
        // Assert
        response.EnsureSuccessStatusCode();
        content.RegistryPhoneNumber.Should().Be(command.RegistryPhoneNumber);
        content.Address.Should().BeEquivalentTo(command.AddressRequestDto.ToString());
        content.IsActive.Should().Be(command.IsActive);
    }

    [Fact]
    public async Task CreateOffice_ReturnsBadRequest_WhenAddressIsInvalid()
    {
        // Arrange
        var command = Utilities.GenerateInvalidOfficeCommand(true, false);
        
        // Act
        var response = await _client.PostAsJsonAsync("api/offices/", command);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task CreateOffice_ReturnsBadRequest_WhenPhoneNumberIsInvalid()
    {
        // Arrange
        var command = Utilities.GenerateInvalidOfficeCommand(false, true);
        
        // Act
        var response = await _client.PostAsJsonAsync("api/offices/", command);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    [Fact]
    public async Task CreateOffice_ReturnsBadRequest_WhenBothAddressAndPhoneNumberAreInvalid()
    {
        // Arrange
        var command = Utilities.GenerateInvalidOfficeCommand(true, true);
        
        // Act
        var response = await _client.PostAsJsonAsync("api/offices/", command);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }
    
    private async Task SeedOffices(IEnumerable<Office> offices)
    {
        await using var scope = _fixture.Services.CreateAsyncScope();
        var readRepository = scope.ServiceProvider.GetRequiredService<IReadOfficesRepository>();
        var writeRepository = scope.ServiceProvider.GetRequiredService<IWriteOfficesRepository>();

        var tasks = new List<Task>();
        foreach (var office in offices)
        {
            tasks.Add(readRepository.AddAsync(office, CancellationToken.None));
            tasks.Add(writeRepository.AddOfficeAsync(office, CancellationToken.None));
        }
        
        await Task.WhenAll(tasks);
        await writeRepository.SaveChangesAsync();
    }
