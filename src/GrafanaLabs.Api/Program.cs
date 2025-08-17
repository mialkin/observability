using GrafanaLabs.Api.Telemetry;
using GrafanaLabs.Api.Telemetry.Meters;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using Serilog.Sinks.OpenTelemetry;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
    configuration.WriteTo.OpenTelemetry(
        endpoint: "http://localhost:4317",
        protocol: OtlpProtocol.Grpc);
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
