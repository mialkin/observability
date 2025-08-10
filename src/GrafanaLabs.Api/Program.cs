using GrafanaLabs.Api.Configurations.Meters;
using GrafanaLabs.Api.Configurations.OpenTelemetry;
using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

builder.ConfigureOpenTelemetry();

var services = builder.Services;
services.ConfigureMeters();

var application = builder.Build();

application.UseSerilogRequestLogging();

application.MapGet("/", () => "GrafanaLabs.Api");

application.MapGet("/counter/increment", (int? value, [FromServices] SimpleMeter meter) =>
{
    if (value is not null)
    {
        meter.Count((int)value);
    }
});

application.Run();
