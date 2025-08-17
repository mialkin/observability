using GrafanaLabs.Api.Configurations;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;

namespace GrafanaLabs.Api.Telemetry;

public static class TelemetryConfiguration
{
    public static void ConfigureTelemetry(this IHostApplicationBuilder applicationBuilder)
    {
        var settings = applicationBuilder.Configuration.GetSettings<TelemetrySettings>();

        applicationBuilder.Services
            .AddOpenTelemetry()
            .ConfigureResource(builder => builder
                .AddService(settings.ServiceName, serviceVersion: settings.ServiceVersion))
            .WithMetrics(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
                .AddRuntimeInstrumentation()
                .AddMeter(OtelScopeName.Default)
                .AddPrometheusExporter()
            );

        applicationBuilder.Services
            .AddSingleton<Instrumentation>(_ =>
                new Instrumentation(
                    activitySourceName: settings.ServiceName,
                    activitySourceVersion: settings.ServiceVersion));
    }
}
