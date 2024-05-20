using Application;
using Application.Abstractions.Services;
using FluentValidation.AspNetCore;
using Infrastructure;
using Infrastructure.Persistence;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Presentation.Middleware;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        if (string.IsNullOrWhiteSpace(environment))
        {
            throw new Exception("ASPNETCORE_ENVIRONMENT environment variable is not set.");
        }
        
        builder.Configuration
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: false)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables()
            .Build();
        
        builder.Services
            .AddApplication(builder.Configuration)
            .AddInfrastructure(builder.Configuration, environment)
            .AddIdentity(builder.Configuration)
            .AddLogging()
            .AddSwagger()
            .AddFluentValidationAutoValidation()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddEndpointsApiExplorer()
            .AddControllers();

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

        try
        { 
            var context = scope.ServiceProvider.GetRequiredService<AuthDbContext>();
            await context.Database.MigrateAsync();

            var seeder = scope.ServiceProvider.GetRequiredService<IDataSeederService>();
            await seeder.SeedRecordsAsync();
        }
        catch (Exception ex)
        {
            var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "Host terminated unexpectedly");
        }
        
        await app.RunAsync();
    }
    
    private static IServiceCollection AddSwagger(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme()
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
        });

        return serviceCollection;
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