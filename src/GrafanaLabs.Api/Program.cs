using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});


var application = builder.Build();

application.UseSerilogRequestLogging();

application.MapGet("/", () => "GrafanaLabs.Api");

application.Run();
