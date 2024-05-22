using Microsoft.AspNetCore.Identity;


namespace AuthTests;

public static class TestUtils
{
    public static void SeedEnvironmentVariables(string dbConnectionString, string redisConnectionString)
    {
        Environment.SetEnvironmentVariable("Test_ConnectionString", dbConnectionString);
        Environment.SetEnvironmentVariable("Test_RedisConnectionString", redisConnectionString);
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Development");
        Environment.SetEnvironmentVariable("Test_Logs", "Disable elasticsearch");
    }
    public static User FakeUser()
    {
        var userFaker = new Faker<User>()
            .RuleFor(x => x.Id, Guid.NewGuid())
            .RuleFor(x => x.EmailConfirmed, false)
            .RuleFor(x => x.Email, "email@mail.com")
            .RuleFor(x => x.PasswordHash, new PasswordHasher<User>().HashPassword(It.IsAny<User>(), "password123-R"));
        
        return userFaker.Generate();
    }
}