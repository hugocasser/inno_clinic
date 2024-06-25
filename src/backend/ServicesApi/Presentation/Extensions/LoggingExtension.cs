using System.Reflection;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

namespace Presentation.Extensions;

public static class LoggingExtension
{
    public static IApplicationBuilder UseLogging(this IApplicationBuilder application)
    {
        if (Environment.GetEnvironmentVariable("ELASTIC_CONNECTION_STRING") != "Disable elasticsearch")
        {
            application.UseSerilogRequestLogging();
        }

        return application;
    }

    public static WebApplicationBuilder AddLoggingServices(
        this WebApplicationBuilder builder)
    {
        if (Environment.GetEnvironmentVariable("ELASTIC_CONNECTION_STRING") != "Disable elasticsearch")
        {
            return builder.AddElasticAndSerilog();
        }

        builder.Logging.ClearProviders();
        builder.Logging.AddConsole();

        return builder;
    }

    private static WebApplicationBuilder AddElasticAndSerilog(
        this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithExceptionDetails()
                .WriteTo.Console()
                .WriteTo.Elasticsearch(ConfigureElasticSink(builder.Configuration))
                .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
                .ReadFrom.Configuration(context.Configuration);
        });

        return builder;
    }

    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration)
    {
        var connectionString = configuration["ElasticConfiguration:Uri"];

        if (string.IsNullOrEmpty(connectionString))
        {
            throw new NullReferenceException("ElasticConfiguration:Uri configuration is empty");
        }

        var connectionUri = new Uri(connectionString);

        return new ElasticsearchSinkOptions(connectionUri)
        {
            AutoRegisterTemplate = true,
            IndexFormat =
                $"{Assembly.GetExecutingAssembly().GetName().Name?.ToLower().Replace('.', '-')}-{DateTime.UtcNow:yyyy-MM}",
            NumberOfReplicas = 1,
            NumberOfShards = 2,
        };
    }
}