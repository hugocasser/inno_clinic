using Application.Services.Specification.Offices;

namespace UnitTests.Specifications;

public class SpecificationsOperators
{
    [Fact]
    public void SpecificationsOperators_AndOperator_ShouldReturnOffices()
    {
        // Arrange
        var office1 = Utilities.GenerateOffice();
        var office2 = Utilities.GenerateOffice();
        office1.Address = office2.Address;
        office1.RegistryPhoneNumber = office2.RegistryPhoneNumber;
        
        var spec1 = new OfficesByNumber(office1.RegistryPhoneNumber);
        var spec2 = new OfficesByAddress(office2.Address);
        var resultSpec = spec1 & spec2;
        
        var offices = Utilities.GenerateOfficesList(10).ToList();
        
        // Act
        var result = offices.Where(resultSpec.ToExpression().Compile());
        
        // Assert
        result.Should().BeEquivalentTo(offices.Where(x => x.RegistryPhoneNumber.StartsWith(office1.RegistryPhoneNumber) && x.Address.StartsWith(office2.Address)));
    }
    
    [Fact]
    public void SpecificationsOperators_OrOperator_ShouldReturnOffices()
    {
        // Arrange
        var office1 = Utilities.GenerateOffice();
        var office2 = Utilities.GenerateOffice();
        
        var spec1 = new OfficesByNumber(office1.RegistryPhoneNumber);
        var spec2 = new OfficesByAddress(office2.Address);
        var resultSpec = spec1 | spec2;
        
        var offices = Utilities.GenerateOfficesList(10).ToList();
        // Act
        var result = offices.Where(resultSpec.ToExpression().Compile());
        
        // Assert
        result.Should().BeEquivalentTo(offices.Where(x => x.RegistryPhoneNumber.StartsWith(office1.RegistryPhoneNumber) || x.Address.StartsWith(office2.Address)));
    }
}