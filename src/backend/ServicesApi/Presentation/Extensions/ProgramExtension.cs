using BLL;
using DLL;
using Presentation.Endpoints;

namespace Presentation.Extensions;

public static class ProgramExtension
{
    public static WebApplicationBuilder ConfigureBuilder(this WebApplicationBuilder builder)
    {
        builder
            .Services
            .AddDataLayer()
            .AddBusinessLayer();
        
        return builder;
    }

    public static WebApplication ConfigureApplication(this WebApplication app)
    {
        app.MapServicesEndpoints();
        app.MapSpecializationsEndpoints();
        
        return app;
    }

    public static Task RunApplicationAsync(this WebApplication app)
    {
        return app.RunAsync();
    }
}