using Application.Dtos.Requests;

namespace UnitTests;

public static class Utilities
{
    private static readonly Fixture _fixture = new ();
    public static Office GenerateOfficeWithPhoto()
    {
        return _fixture
            .Build<Office>()
            .Create();
    }
    
    public static Office GenerateOfficeWithoutPhoto()
    {
        return _fixture
            .Build<Office>()
            .Without(office => office.PhotoId)
            .Create();
    }
    
    public static AddressRequestDto GenerateAddress()
    {
        return _fixture
            .Build<AddressRequestDto>()
            .Create();
    }
    
    public static PageSettings GenerateValidPageSettings()
    {
        return _fixture
            .Build<PageSettings>()
            .Create();
    }
    
    public static PageSettings GenerateNotValidPageSettings()
    {
        return _fixture
            .Build<PageSettings>()
            .With(settings => settings.ItemsPerPage, new Random().Next(-1000 ,0))
            .With(settings => settings.Page, new Random().Next(-1000, 0))
            .Create();
    }
    
    public static PageSettings GenerateNullPageSettings()
    {
        return _fixture
            .Build<PageSettings>()
            .Without(pageSettings => pageSettings.Page)
            .Without(pageSettings => pageSettings.ItemsPerPage)
            .Create();
    }
    
    public static IEnumerable<Office> GenerateOfficesList(int count)
    {
        return _fixture
            .Build<Office>()
            .Without(office => office.PhotoId)
            .CreateMany(count);
    }

    public static Office GenerateOffice()
    {
        return 
            _fixture
                .Build<Office>()
                .Without(office => office.PhotoId)
                .Create();
    }
}