using Application.Services.Specification.Offices;

namespace UnitTests.Specifications;

public class OfficesByNumbersTests
{
    [Fact]
    public void OfficesByNumbers_ShouldReturnOfficesByNumbers()
    {
        // Arrange
        var office = Utilities.GenerateOffice();
        var offices = Utilities.GenerateOfficesList(10).ToList();
        offices.Add(office);
        var spec = new OfficesByNumber(office.RegistryPhoneNumber);
        
        // Act
        var result = offices.Where(spec.ToExpression().Compile());
        
        // Assert
        result.Should().BeEquivalentTo(offices.Where(x => x.RegistryPhoneNumber.StartsWith(office.RegistryPhoneNumber)));
    }
}