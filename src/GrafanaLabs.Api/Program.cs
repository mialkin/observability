using GrafanaLabs.Api.Configurations.OpenTelemetry;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

builder.ConfigureOpenTelemetry();

var application = builder.Build();

application.UseSerilogRequestLogging();

application.MapGet("/", () => "GrafanaLabs.Api");

application.Run();
