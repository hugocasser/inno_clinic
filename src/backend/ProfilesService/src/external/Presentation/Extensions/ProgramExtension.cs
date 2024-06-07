using System.Net.Mime;
using System.Reflection;
using Application;
using FastEndpoints;
using FastEndpoints.Swagger;
using Infrastructure;
using Infrastructure.Persistence.Write;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Presentation.Common;
using Presentation.Middlewares;

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
            .AddIdentity()
            .AddLogging()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddFastEndpoints()
            .SwaggerDocument()
            .AddControllers();

        builder.AddLoggingServices();
        
        return builder;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseMiddleware<ErrorHandlerMiddleware>();
        app.UseLogging();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Profiles v1");
                c.RoutePrefix = "swagger";
            });
        }
        
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseCors("AllowAll");
        app.UseFastEndpoints(config =>
        {
            config.Endpoints.RoutePrefix = "api";
            config.Endpoints.ShortNames = true;
        });

        app.MapControllers();
        
        return app;
    }
    
    public static async Task RunApplicationAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        
        var context = services.GetRequiredService<ProfilesWriteDbContext>();
        await context.Database.MigrateAsync();

        await DataSeeder.SeedStatuses(context);
        
        await app.RunAsync();
    }
    
    private static CorsOptions ConfigureAllowAllCors(this CorsOptions options)
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });

        return options;
    }
}