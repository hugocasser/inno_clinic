using Application;
using Infrastructure;
using Microsoft.AspNetCore.Cors.Infrastructure;
using Presentation.Endpoints;
using Presentation.Middleware;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddApplication()
            .AddInfrastructure()
            .AddLogging()
            .AddEndpointsApiExplorer()
            .AddSwaggerGen()
            .AddCors(options => options.ConfigureAllowAllCors());
        
        builder.AddLoggingServices();
        
        return builder;
    }
    
    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.UseMiddleware<ExceptionHandlerMiddleware>();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Profiles v1");
                c.RoutePrefix = "swagger";
            });
        }

        app.UseCors("AllowAll");
        app.MapCreationEndpoints();
        app.MapUpdateEndpoints();
        app.MapDeleteEndpoints();
        
        return app;
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