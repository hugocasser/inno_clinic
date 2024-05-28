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
    
    public static PageSettings GeneratePageSettings()
    {
        return _fixture
            .Build<PageSettings>()
            .Create();
    }
    
    public static IEnumerable<Office> GenerateOfficesList(int count)
    {
        return _fixture
            .Build<Office>()
            .Without(office => office.PhotoId)
            .CreateMany(count);
    }
}