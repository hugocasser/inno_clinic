using Application.Services.Specification.Offices;

namespace UnitTests.Specifications;

public class ActiveOfficesTests
{
    [Fact]
    public void ActiveOffices_ShouldReturnActiveOffices()
    {
        // Arrange
        var spec = new ActiveOffices();
        var offices = Utilities.GenerateOfficesList(10);
        
        // Act
        var result = offices.Where(spec.ToExpression().Compile());
        
        // Assert
        result.Should().BeEquivalentTo(offices.Where(x => x.IsActive));
    }
}