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
                .AddPrometheusExporter()
                .AddMeter(OtelScopeName.Default));
        // .WithTracing(builder => builder
        //     .AddSource(settings.ServiceName)
        //     .AddAspNetCoreInstrumentation()
        //     .AddOtlpExporter(x => x.Endpoint = new Uri(settings.JaegerUrl)));

        // applicationBuilder.Logging
        //     .AddOpenTelemetry(options =>
        //     {
        //         var builder = ResourceBuilder
        //             .CreateDefault()
        //             .AddService(settings.ServiceName, serviceVersion: settings.ServiceVersion);
        //
        //         options.SetResourceBuilder(builder);
        //     });

        applicationBuilder.Services
            .AddSingleton<Instrumentation>(_ =>
                new Instrumentation(
                    activitySourceName: settings.ServiceName,
                    activitySourceVersion: settings.ServiceVersion));
    }
}
