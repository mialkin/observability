using System.Net;
using Microsoft.AspNetCore.Mvc;
using OpenTelemetry.Traces;
using OpenTelemetry.Traces.Configurations.OpenTelemetry;
using Serilog;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureOpenTelemetry();

builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);
    configuration.WriteTo.Console();
});

var services = builder.Services;

services.AddSingleton<Dice>();

var application = builder.Build();

application.MapGet("/", () => "OpenTelemetry.Traces");

application.MapGet("dice/roll", (string? player, int? rolls, [FromServices] Dice dice, ILogger<Program> logger) =>
{
    if (!rolls.HasValue)
    {
        logger.LogError("Missing rolls parameter");
        throw new HttpRequestException(
            message: "Missing rolls parameter",
            inner: null,
            statusCode: HttpStatusCode.BadRequest);
    }

    var result = dice.Roll(rolls.Value);

    if (string.IsNullOrWhiteSpace(player))
    {
        logger.LogInformation("Anonymous player is rolling the dice: {result}", result);
    }
    else
    {
        logger.LogInformation("{player} is rolling the dice: {result}", player, result);
    }

    return result;
});

application.Run();
