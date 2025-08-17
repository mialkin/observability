using GrafanaLabs.Api.Configurations;
using OpenTelemetry;
using OpenTelemetry.Exporter;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

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
            )
            .WithTracing(builder => builder
                .AddAspNetCoreInstrumentation()
                .AddHttpClientInstrumentation()
            )
            .UseOtlpExporter(protocol: OtlpExportProtocol.Grpc, baseUrl: new Uri("http://localhost:6510"));

        applicationBuilder.Services
            .AddSingleton<Instrumentation>(_ =>
                new Instrumentation(
                    activitySourceName: settings.ServiceName,
                    activitySourceVersion: settings.ServiceVersion));
    }
}
