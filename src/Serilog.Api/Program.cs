using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
    // configuration.WriteTo.File("/Users/john.doe/Downloads/serilog-api.log");
});

var application = builder.Build();

application.UseSerilogRequestLogging();

application.MapGet(
    pattern: "/",
    handler: ([FromServices] ILogger<Program> logger) =>
    {
        logger.LogInformation("Hello");
        return Results.Ok("Hello");
    });

application.Run();
