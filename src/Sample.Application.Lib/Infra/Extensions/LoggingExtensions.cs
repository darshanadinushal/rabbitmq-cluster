using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Elasticsearch;
using Serilog.Sinks.Elasticsearch;
using Serilog.Sinks.SystemConsole.Themes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sample.Application.Lib.Infra.Extensions
{
    public static class LoggingExtensions
    {
        public static IHostBuilder UseLogging(this IHostBuilder hostBuilder, string applicationName = null) => hostBuilder
    .UseSerilog((context, loggerConfiguration) =>
    {
        var logLevel = context.Configuration.GetValue<string>("Logging:MinimumLevel"); // read level from appsettings.json
        if (!Enum.TryParse<LogEventLevel>(logLevel, true, out var level))
        {
            level = LogEventLevel.Information; // or set default value
        }

        // get application name from appsettings.json
        applicationName = string.IsNullOrWhiteSpace(applicationName) ? context.Configuration.GetValue<string>("Application:Name") : applicationName;

        loggerConfiguration.Enrich
            .FromLogContext()
            .MinimumLevel.Is(level)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("System", LogEventLevel.Warning)
            .Enrich.WithProperty("Environment", context.HostingEnvironment.EnvironmentName)
            .Enrich.WithProperty("ApplicationName", applicationName);

        // read other Serilog configuration
        loggerConfiguration.ReadFrom.Configuration(context.Configuration);

        // get output format from appsettings.json. 
        var outputFormat = context.Configuration.GetValue<string>("Logging:OutputFormat");


        switch (outputFormat)
        {
            case "filebeat-eck":
                loggerConfiguration.WriteTo.Console(new ElasticsearchJsonFormatter());
                break;
            case "elasticsearch":
                {
                    loggerConfiguration.WriteTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri("https://elasticsearch-ccdp-es-http:9200"))
                    {
                        ModifyConnectionSettings = x => x.BasicAuthentication("elastic", "4S4Q2arsH4Lb2lO20845FVxx"),
                        IndexFormat = "application-pub-" + $"{DateTime.UtcNow:yyyy-MM}",
                        AutoRegisterTemplate = true,
                        DetectElasticsearchVersion = true,
                        RegisterTemplateFailure = RegisterTemplateRecovery.IndexAnyway
                    });
                }
                break;
            default:
                loggerConfiguration.WriteTo.Console(
                    theme: AnsiConsoleTheme.Code,
                    outputTemplate: "[{Timestamp:yy-MM-dd HH:mm:ss.sssZ} {Level:u3}] {Message:lj} <s:{Environment}/{ApplicationName}/{SourceContext}>{NewLine}{Exception}");
                break;
        }
    });
    }
}
