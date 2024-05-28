using Application;
using Infrastructure;
using Infrastructure.Abstractions;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Presentation.Middleware;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static WebApplicationBuilder ConfigureBuilder
        (this WebApplicationBuilder builder)
    
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
        builder.Configuration
            .AddJsonFile("appsettings.json", false, false)
            .AddJsonFile($"appsettings.{environment}.json", true, false);
        
        builder
            .Services
            .AddApplication()
            .AddInfrastructure()
            .AddControllers();
        
        builder
            .Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();

        builder.AddLoggingServices();
        
        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseMiddleware<CustomExceptionHandlerMiddleware>();
        app.UseLogging();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo v1");
                c.RoutePrefix = "swagger";
            });
        }
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        app.MapControllers();
        
        return app;
    }
    
    public static async Task RunApplicationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<OfficesWriteDbContext>();
        
        await context.Database.MigrateAsync();
        
        
        await app.RunAsync();
    }
}