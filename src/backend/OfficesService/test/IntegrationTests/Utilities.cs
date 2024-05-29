using Application.Dtos.Requests;
using Application.Request.Commands.CreateOffice;
using Domain.Models;

namespace IntegrationTests;

public static class Utilities
{
    private static readonly Fixture _fixture = new();
    public static void SeedTestEnvironmentVariables(string postgresConnectionString)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        Environment.SetEnvironmentVariable("POSTGRES_CONNECTION_STRING", postgresConnectionString);
        Environment.SetEnvironmentVariable("ELASTIC_CONNECTION_STRING", "Disable_elasticsearch");
    }
    
    public static PageSettings GenerateValidPageSettings()
    {
        return new PageSettings(1, 10);
    }
    
    public static PageSettings GenerateInvalidPageSettings()
    {
        return new PageSettings(-3, -3);
    }
    
    public static AddressRequestDto GenerateValidAddressRequestDto()
    {
        return new AddressRequestDto("Street", "City", "State", "Country", "PostalCode");
    }
    
    public static AddressRequestDto? GenerateInvalidAddressRequestDto()
    {
        return new AddressRequestDto("d", "error", "State", "Country", "PostalCode");
    }
    
    public static string ValidPhoneNumber()
    {
        return "1234567890";
    }

    private static string InvalidPhoneNumber()
    {
        return "error";
    }
    
    public static IEnumerable<Office> GenerateOfficesList(int count)
    {
        var offices = _fixture.Build<Office>()
            .With(office => office.RegistryPhoneNumber, ValidPhoneNumber() + new Random().Next(1, 99))
            .CreateMany(count);
        
        return offices;
    }
    
    public static CreateOfficeCommand GenerateOfficeCommand()
    {
        return 
            _fixture
                .Build<CreateOfficeCommand>()
                .With(office => office.AddressRequestDto, GenerateValidAddressRequestDto())
                .With(office => office.RegistryPhoneNumber, ValidPhoneNumber() + new Random().Next(1, 99))
                .Create();
    }

    public static CreateOfficeCommand GenerateInvalidOfficeCommand(bool address, bool phoneNumber)
    {
        var addressRequestDto = address ? GenerateInvalidAddressRequestDto()! : GenerateValidAddressRequestDto();
        var registryPhoneNumber = phoneNumber ? InvalidPhoneNumber() : ValidPhoneNumber();
        
        return 
            _fixture
                .Build<CreateOfficeCommand>()
                .With(office => office.AddressRequestDto, addressRequestDto)
                .With(office => office.RegistryPhoneNumber, registryPhoneNumber)
                .Create();
    }
}