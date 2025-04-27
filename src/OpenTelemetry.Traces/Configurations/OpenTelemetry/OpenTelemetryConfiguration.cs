using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace OpenTelemetry.Traces.Configurations.OpenTelemetry;

public static class OpenTelemetryConfiguration
{
    public static void ConfigureOpenTelemetry(this IHostApplicationBuilder builder)
    {
        var settings = builder.Configuration.GetSettings<OpenTelemetrySettings>();

        builder.Services.AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService(
                serviceName: settings.ServiceName,
                serviceVersion: settings.ServiceVersion))
            .WithTracing(tracing => tracing
                .AddSource(settings.ServiceName)
                .AddAspNetCoreInstrumentation()
                .AddConsoleExporter())
            .WithMetrics(metrics => metrics
                .AddMeter(settings.ServiceName)
                .AddConsoleExporter());

        builder.Logging.AddOpenTelemetry(options => options
            .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService(
                serviceName: settings.ServiceName,
                serviceVersion: settings.ServiceVersion))
            .AddConsoleExporter());

        builder.Services.AddSingleton<Instrumentation>(_ =>
            new Instrumentation(
                activitySourceName: settings.ServiceName,
                activitySourceVersion: settings.ServiceVersion));
    }
}