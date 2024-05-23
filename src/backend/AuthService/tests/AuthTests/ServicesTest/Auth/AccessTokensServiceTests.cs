using Application.Options;

namespace AuthTests.ServicesTest.Auth;

[Collection("UnitTest")]
public class AccessTokensServiceTests : UnitTestFixtures
{
    private readonly User User = TestUtils.FakeUser();
    
    public AccessTokensServiceTests()
    {
        MockAccessOptions
            .Setup(options => options.Value)
            .Returns(new AccessTokenOptions
            {
                Audience = "audience",
                Issuer = "issuer",
                Key = "0567e065-e6a5-4c25-aa36-e8304303b14b"

            });
        AccessTokensService = new AccessTokensService(MockAccessOptions.Object, UserManagerMock.Object);
    }
    

    [Fact]
    public  void CreateAccessToken_ShouldBeCreated_WhenRolesNotNull()
    {
        // Arrange
        var roles = new List<string>() {"role1", "role2"};
        
        UserManagerMock
            .Setup(manager => manager.GetRolesAsync(It.IsAny<User>()))
            .ReturnsAsync(roles);
        
        // Act
        var result =  AccessTokensService.CreateAccessToken(User, roles);
        
        // Assert
        result.Should().NotBeNullOrEmpty();
    }
    
    [Fact]
    public void CreateAccessToken_ShouldReturnEmpty_WhenRolesNull()
    {
        // Act
        var result =  AccessTokensService.CreateAccessToken(User, null);
        
        // Assert
        result.Should().BeNullOrEmpty();
    }
}