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
}