using Microsoft.AspNetCore.Mvc;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
});

var application = builder.Build();

application.UseSerilogRequestLogging();

application.MapGet(
    pattern: "/",
    handler: ([FromServices] ILogger<Program> logger) =>
    {
        using (logger.BeginScope("Processing order {OrderId}", "E-000003456"))
        {
            logger.LogInformation("Hello");
            logger.LogInformation("Hello World");
        }

        logger.LogInformation("Bye");

        return Results.Ok("Success");
    });

application.Run();
