using Application.Services.Specification.Offices;

namespace UnitTests.Specifications;

public class OfficesByAddressTests
{
    [Fact]
    public void OfficesByAddress_ShouldReturnOfficesByAddress()
    {
        // Arrange
        var office = Utilities.GenerateOffice();
        var offices = Utilities.GenerateOfficesList(10).ToList();
        offices.Add(office);
        var spec = new OfficesByAddress(office.Address);
        
        // Act
        var result = offices.Where(spec.ToExpression().Compile());
        
        // Assert
        result.Should().BeEquivalentTo(offices.Where(x => x.Address.StartsWith(office.Address)));
    }
}