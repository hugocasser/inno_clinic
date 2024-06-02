using Application;
using Infrastructure;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables();
        
        builder.Services
            .AddApplication()
            .AddInfrastructure()
            .AddLogging()
            .AddIdentity()
            .AddControllers();
        
        return builder;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        return app;
    }
    
    public static async Task RunApplicationAsync(this WebApplication app)
    {
        await app.RunAsync();
    }
    
    
}