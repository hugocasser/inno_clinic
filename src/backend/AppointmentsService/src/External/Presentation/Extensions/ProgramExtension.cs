using Application;
using Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;

namespace Presentation.Extensions;

public static class ProgramExtension
{
        public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();

        builder
            .Services
            .AddApplication()
            .AddInfrastructure()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddAuthorization()
            .AddAuthentication();
        
        return builder;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        
        return app;
    }
    
    public static Task RunApplicationAsync(this WebApplication app)
    {
        return app.RunAsync();
    }
    
    private static void ConfigureAllowAllCors(this CorsOptions options)
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
    }
}