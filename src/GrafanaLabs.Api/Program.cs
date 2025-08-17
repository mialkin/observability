using GrafanaLabs.Api.Telemetry;
using GrafanaLabs.Api.Telemetry.Meters;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Exporter;
using OpenTelemetry.Logs;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.AddOpenTelemetry(logging =>
{
    // The rest of your setup code goes here
    logging.AddOtlpExporter(x =>
        {
            x.Endpoint = new Uri("http://localhost:4317");
            x.Protocol = OtlpExportProtocol.Grpc;
        })
        // .AddConsoleExporter()
        ;
});

builder.ConfigureTelemetry();

var services = builder.Services;
services.ConfigureMeters();

var application = builder.Build();


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
