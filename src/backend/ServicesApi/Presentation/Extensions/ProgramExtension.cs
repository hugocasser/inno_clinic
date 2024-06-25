using BLL;
using DLL;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Presentation.Endpoints;
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
            .AddDataLayer()
            .AddBusinessLayer()
            .AddCors(options => options.ConfigureAllowAllCors())
            .AddEndpointsApiExplorer()
            .AddSwaggerGen();
            // .AddAuthorization()
            // .AddAuthentication();

        builder.AddLoggingServices();
        
        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app
            .UseMiddleware<ExceptionHandlerMiddleware>()
            .UseLogging()
            .UseMiddleware<ValidationMiddleware>();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Profiles v1");
                c.RoutePrefix = "swagger";
            });
        }
        // app.UseAuthentication();
        // app.UseAuthorization();
        app.UseCors("AllowAll");
        
        app
            .MapServicesEndpoints()
            .MapSpecializationsEndpoints()
            .MapCategoriesEndpoints();
        
        return app;
    }

    public static Task RunApplicationAsync(this WebApplication app)
    {
        return app.RunAsync();
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