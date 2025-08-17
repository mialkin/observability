using GrafanaLabs.Api.Telemetry;
using GrafanaLabs.Api.Telemetry.Meters;
using Microsoft.AspNetCore.Mvc;
using Serilog;

// using Serilog.Formatting.Json;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
    // configuration.WriteTo.File(
    //     formatter: new JsonFormatter(),
    //     path: "/Users/aleksei/logs/dotnet/grafana-labs-api.log");
});

builder.ConfigureTelemetry();

var services = builder.Services;
services.ConfigureMeters();

var application = builder.Build();

application.UseSerilogRequestLogging();
application.UseOpenTelemetryPrometheusScrapingEndpoint();

application.MapGet("/", () => "GrafanaLabs.Api");

application.MapGet("/counter/increment", (int? value, [FromServices] SimpleMeter meter) =>
{
    if (value is not null)
    {
        meter.Count((int)value);
    }
});

application.Run();
